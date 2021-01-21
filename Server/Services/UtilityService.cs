using BlazorWasm.Server.Data;
using BlazorWasm.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorWasm.Server.Services
{
  public class UtilityService : IUtilityService
  {
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UtilityService(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
      _context = context;
      _httpContextAccessor = httpContextAccessor;
    }
    public async Task<User> GetUser()
    {
      var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)); 
      var user = await _context.Users.FirstOrDefaultAsync(usr => usr.Id == userId);

      return user;
    }
  }
}
