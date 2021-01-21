using System;
using System.Threading.Tasks;

namespace BlazorWasm.Client.Services
{
  public interface IBananaService
  {
    event Action OnBananaChange;
    int Bananas { get; set; }

    void EatBananas(int amount);
    Task EarnBananas(int amount);
    Task GetBananas();
  }
}
