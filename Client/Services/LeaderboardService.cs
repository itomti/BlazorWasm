using BlazorWasm.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorWasm.Client.Services
{
  public class LeaderboardService : ILeaderboardService
  {
    private readonly HttpClient _http;
    public IList<UserStatistic> Leaderboard { get; set; }

    public LeaderboardService(HttpClient http)
    {
      _http = http;
    }
    public async Task GetLeaderboard()
    {
      Leaderboard = await _http.GetFromJsonAsync<IList<UserStatistic>>("api/user/leaderboard");
    }
  }
}
