using BlazorWasm.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace BlazorWasm.Server.Data
{
  public class AuthRepository : IAuthRepository
  {
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;

    public AuthRepository(DataContext context, IConfiguration configuration)
    {
      _context = context;
      _configuration = configuration;
    }
    public async Task<ServiceResponse<string>> Login(string email, string password)
    {
      var response = new ServiceResponse<string>();

      User user = await _context.Users.FirstOrDefaultAsync(usr => usr.Email.ToLower().Equals(email.ToLower()));

      if (user == null)
      {
        response.Success = false;
        response.Message = $"User not found.";
      }
      else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
      {
        response.Success = false;
        response.Message = $"User password is incorrect.";
      }
      else
      {
        response.Data = CreateToken(user);
        response.Success = true;
        response.Message = $"Successfully authenticated.";
      }

      return response;
    }

    public async Task<ServiceResponse<int>> Register(User user, string password, int startUnitId)
    {
      if (await UserExists(user.Email))
      {
        return new ServiceResponse<int>
        {
          Success = false,
          Message = "User already exists."
        };
      }

      CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      await _context.Users.AddAsync(user);
      await _context.SaveChangesAsync();

      await AddStartingUnit(user, startUnitId);

      return new ServiceResponse<int>
      {
        Data = user.Id,
        Success = true,
        Message = $"User with username {user.Username} successfully registered."
      };
    }

    private async Task AddStartingUnit(User user, int startUnitId)
    {
      var unit = await _context.Units.FirstOrDefaultAsync<Unit>(u => u.Id == startUnitId);

      await _context.UserUnits.AddAsync(new UserUnit
      {
        UnitId = unit.Id,
        UserId = user.Id,
        Health = unit.Health
      });

      await _context.SaveChangesAsync();
    }

    public async Task<bool> UserExists(string email)
    {
      if (await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower()))
      {
        return true;
      }

      return false;
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      using (var hmac = new HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
      }
    }

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
      using (var hmac = new HMACSHA512(passwordSalt))
      {
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != passwordHash[i])
          {
            return false;
          }
        }
      }

      return true;
    }

    private string CreateToken(User user)
    {
      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username)
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
      var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);

      var jwt = new JwtSecurityTokenHandler().WriteToken(token);

      return jwt;
    }
  }
}
