using BlazorWasm.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorWasm.Client.Services
{
  public class UnitService : IUnitService
  {
    private readonly IToastService _toastService;
    private readonly IBananaService _bananaService;
    private readonly HttpClient _http;
    public IList<Unit> Units { get; set; } = new List<Unit>();
    public IList<UserUnit> UserUnits { get; set; } = new List<UserUnit>();
    public UnitService(IToastService toastService, HttpClient http, IBananaService bananaService)
    {
      _toastService = toastService;
      _http = http;
      _bananaService = bananaService;
    }
    public async Task AddUnit(int unitId)
    {
      Unit unit = Units.First(unit => unit.Id == unitId);
      var result = await _http.PostAsJsonAsync<int>("api/UserUnit", unitId);

      if (result.StatusCode != System.Net.HttpStatusCode.OK)
      {
        _toastService.ShowError(await result.Content.ReadAsStringAsync(), ":(");
      }
      else
      {
        await _bananaService.GetBananas();
        _toastService.ShowSuccess($"Successfully built unit {unit.Title}", ":)");
      }

    }

    public async Task LoadUnitsAsync()
    {
      if (Units.Count == 0)
      {
        Units = await _http.GetFromJsonAsync<IList<Unit>>("api/unit");
      }
    }

    public async Task LoadUserUnitsAsync()
    {
      UserUnits = await _http.GetFromJsonAsync<IList<UserUnit>>("api/userunit");
    }

    public async Task ReviveArmy()
    {
      var result = await _http.PostAsJsonAsync<string>("api/userunit/revive", null);
      if (result.StatusCode == System.Net.HttpStatusCode.OK)
      {
        _toastService.ShowSuccess(await result.Content.ReadAsStringAsync());
      }
      else
      {
        _toastService.ShowError(await result.Content.ReadAsStringAsync());
      }

      await LoadUserUnitsAsync();
      await _bananaService.GetBananas();
    }
  }
}
