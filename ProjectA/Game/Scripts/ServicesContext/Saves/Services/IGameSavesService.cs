using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Saves.Datas;
using GUtils.Loading.Loadables;
using GUtils.Optionals;

namespace Game.ServicesContext.Saves.Services;

public interface IGameSavesService : ILoadable
{
    void SetCurrentSaveDataSlot(int slot);
    Optional<GameSaveData> GetCurrentSaveData();
    Task SaveCurrentSaveData(CancellationToken cancellationToken);
}