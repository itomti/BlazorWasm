using BlazorWasm.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorWasm.Client.Services
{
  public class AuthService : IAuthService
  {
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<ServiceResponse<string>> Login(BlazorWasm.Shared.UserLogin request)
    {
      var result = await _httpClient.PostAsJsonAsync("api/auth/login", request);

      var content = await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();

      return content;
    }

    public async Task<ServiceResponse<int>> Register(UserRegister request)
    {
      var result = await _httpClient.PostAsJsonAsync("api/auth/register", request);
      
      var content = await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();

      return content;
    }
  }
}
