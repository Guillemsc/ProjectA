using System.Threading;
using System.Threading.Tasks;
using Game.ServicesContext.Saves.Services;

namespace Game.GameContext.Saves.UseCases;

public sealed class SaveGameMapSaveDataUseCase
{
    readonly IGameSavesService _gameSavesService;

    public SaveGameMapSaveDataUseCase(
        IGameSavesService gameSavesService
        )
    {
        _gameSavesService = gameSavesService;
    }

    public Task Execute(CancellationToken cancellationToken)
    {
        return _gameSavesService.SaveCurrentSaveData(cancellationToken);
    }
}