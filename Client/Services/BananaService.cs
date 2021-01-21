using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorWasm.Client.Services
{
  public class BananaService : IBananaService
  {
    private readonly HttpClient _http;

    public event Action OnBananaChange;
    public int Bananas { get; set; } = 1000;

    public BananaService(HttpClient http)
    {
      _http = http;
    }

    public void EatBananas(int amount)
    {
      Bananas -= amount;
      BananasChanged();
    }

    public async Task EarnBananas(int amount)
    {
      var result = await _http.PutAsJsonAsync<int>("api/User/AddBananas", amount);
      Bananas = await result.Content.ReadFromJsonAsync<int>();
      BananasChanged();
    }

    private void BananasChanged()
    {
      OnBananaChange?.Invoke();
    }

    public async Task GetBananas()
    {
      Bananas = await _http.GetFromJsonAsync<int>("api/User/GetBananas");
      BananasChanged();
    }
  }
}
