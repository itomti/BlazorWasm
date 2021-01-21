using BlazorWasm.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWasm.Client.Services
{
  public interface IUnitService
  {
    IList<Unit> Units { get; set; }
    IList<UserUnit> UserUnits { get; set; }
    Task AddUnit(int unitId);
    Task LoadUnitsAsync();
    Task LoadUserUnitsAsync();
    Task ReviveArmy();
  }
}
