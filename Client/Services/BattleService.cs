using BlazorWasm.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorWasm.Client.Services
{
  public class BattleService : IBattleService
  {
    private readonly HttpClient _http;
    public IList<BattleHistoryEntry> History { get; set; } = new List<BattleHistoryEntry>();

    public BattleResult LastBattle { get; set; } = new BattleResult();
    public BattleService(HttpClient http)
    {
      _http = http;
    }

    public async Task<BattleResult> StartBattle(int opponentId)
    {
      var result = await _http.PostAsJsonAsync("api/Battle", opponentId);

      LastBattle = await result.Content.ReadFromJsonAsync<BattleResult>();
      return LastBattle;
    }

    public async Task GetHistory()
    {
      History = await _http.GetFromJsonAsync<BattleHistoryEntry[]>("api/Battle/History");
    }
  }
}
