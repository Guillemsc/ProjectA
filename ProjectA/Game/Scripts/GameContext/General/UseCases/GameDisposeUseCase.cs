using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Saves.UseCases;

namespace Game.GameContext.General.UseCases;

public sealed class GameDisposeUseCase
{
    readonly SaveGameMapSaveDataUseCase _saveGameMapSaveDataUseCase;

    public GameDisposeUseCase(
        SaveGameMapSaveDataUseCase saveGameMapSaveDataUseCase
        )
    {
        _saveGameMapSaveDataUseCase = saveGameMapSaveDataUseCase;
    }

    public Task Execute(CancellationToken cancellationToken)
    {
        return _saveGameMapSaveDataUseCase.Execute(cancellationToken);
    }
}