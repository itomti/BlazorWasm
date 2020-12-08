using BlazorWasm.Shared;
using System.Collections.Generic;

namespace BlazorWasm.Client.Services
{
  public interface IUnitService
  {
    IList<Unit> Units { get; }
    IList<UserUnit> UserUnits { get; set; }
    void AddUnit(int unitId);
  }
}
