using BlazorWasm.Server.Data;
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
  public class UnitController : ControllerBase
  { 
    private readonly DataContext _context;
    public UnitController(DataContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetUnitsAsync()
    {
      var units = await _context.Units.ToListAsync();
      return Ok(units);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddUnitAsync(Unit unit)
    {
      await _context.Units.AddAsync(unit);
      await _context.SaveChangesAsync();
      return Ok(await _context.Units.ToListAsync());
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateUnitAsync(int id, Unit unit)
    {
      Unit dbUnit = await _context.Units.FirstOrDefaultAsync(unit => unit.Id == id);
      if (dbUnit == null)
      {
        return NotFound($"Unit with given id {id} does not exist.");
      }

      dbUnit.Title = unit.Title;
      dbUnit.Attack = unit.Attack;
      dbUnit.Defense = unit.Defense;
      dbUnit.Cost = unit.Cost;
      dbUnit.Health = unit.Health;

      await _context.SaveChangesAsync();

      return Ok(dbUnit);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUnitAsync(int id)
    {
      Unit dbUnit = await _context.Units.FirstOrDefaultAsync(unit => unit.Id == id);
      if (dbUnit == null)
      {
        return NotFound($"Unit with given id {id} does not exist.");
      }

      _context.Units.Remove(dbUnit);
      await _context.SaveChangesAsync();

      return Ok(await _context.Units.ToListAsync());
    }
  }
}
