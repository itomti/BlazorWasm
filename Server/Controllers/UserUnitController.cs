using BlazorWasm.Server.Data;
using BlazorWasm.Server.Services;
using BlazorWasm.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasm.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class UserUnitController : ControllerBase
  {
    private readonly DataContext _context;
    private readonly IUtilityService _utilityService;

    public UserUnitController(DataContext context, IUtilityService utilityService)
    {
      _context = context;
      _utilityService = utilityService;
    }

    [HttpPost]
    public async Task<IActionResult> BuildUserUnit([FromBody] int unitId)
    {
      var unit = await _context.Units.FirstOrDefaultAsync<Unit>(unit => unit.Id == unitId);
      var user = await _utilityService.GetUser();

      if (user.Bananas < unit.Cost)
      {
        return BadRequest("Not enough bananas!");
      }

      user.Bananas -= unit.Cost;

      UserUnit userUnit = new UserUnit
      {
        UnitId = unit.Id,
        UserId = user.Id,
        Health = unit.Health
      };

      await _context.UserUnits.AddAsync(userUnit);
      await _context.SaveChangesAsync();

      return Ok(userUnit);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserUnit()
    {
      var user = await _utilityService.GetUser();
      var userUnits = await _context.UserUnits.Where(unit => unit.UserId == user.Id).ToListAsync();

      var response = userUnits.Select(
        unit => new UserUnitResponse
        {
          UnitId = unit.UnitId,
          Health = unit.Health
        });

      return Ok(response);
    }
    [HttpPost("revive")]
    public async Task<IActionResult> ReviveArmy()
    {
      var user = await _utilityService.GetUser();
      var userUnits = await _context.UserUnits
        .Where(unit => unit.UserId == user.Id)
        .Include(unit => unit.Unit)
        .ToListAsync();

      int bananaCost = 1000;

      if (user.Bananas < bananaCost)
      {
        return BadRequest("Not enough bananas to revive your army.");
      }

      bool armyAlreadyAlive = true;

      foreach (var userUnit in userUnits)
      {
        if (userUnit.Health <= 0)
        {
          armyAlreadyAlive = false;
          userUnit.Health = new Random().Next(1, userUnit.Unit.Health);
        }
      }

      if (armyAlreadyAlive)
      {
        return Ok("Your army is already alive.");
      }

      user.Bananas -= bananaCost;
      await _context.SaveChangesAsync();

      return Ok("Army revived");
    }
  }
}
