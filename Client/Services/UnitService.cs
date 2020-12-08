using BlazorWasm.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Toast.Services;

namespace BlazorWasm.Client.Services
{
  public class UnitService : IUnitService
  {
    private readonly IToastService _toastService;
    public UnitService(IToastService toastService)
    {
      _toastService = toastService;
    }
    public IList<Unit> Units { get; } = new List<Unit>
    {
      new Unit { Id = 1, Title = "Knight", Attack = 10, Defense = 10, Cost = 100, Health = 300 },
      new Unit { Id = 2, Title = "Archer", Attack = 20, Defense = 5, Cost = 150, Health = 150 },
      new Unit { Id = 3, Title = "Mage", Attack = 30, Defense = 1, Cost = 250, Health = 100 }
    };

    public IList<UserUnit> UserUnits { get; set; } = new List<UserUnit>();

    public void AddUnit(int unitId)
    {
      Unit unit = Units.First(unit => unit.Id == unitId);
      UserUnits.Add(new UserUnit { UnitId = unit.Id, Health = unit.Health });
      _toastService.ShowSuccess($"Successfully built unit {unit.Title}", ":)");
    }
  }
}
