using BlazorWasm.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasm.Client.Services
{
  public interface IBattleService
  {
    IList<BattleHistoryEntry> History { get; set; }
    BattleResult LastBattle { get; set; }
    Task<BattleResult> StartBattle(int opponentId);
    Task GetHistory();
  }
}
