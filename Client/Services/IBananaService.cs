using System;

namespace BlazorWasm.Client.Services
{
  public interface IBananaService
  {
    event Action OnChange;
    int Bananas { get; set; }

    void EatBananas(int amount);
  }
}
