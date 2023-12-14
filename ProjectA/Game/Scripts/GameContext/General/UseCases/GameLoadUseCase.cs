using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Player.UseCases;

namespace Game.GameContext.General.UseCases;

public sealed class GameLoadUseCase
{
    readonly SpawnPlayerUseCase _spawnPlayerUseCase;

    public GameLoadUseCase(SpawnPlayerUseCase spawnPlayerUseCase)
    {
        _spawnPlayerUseCase = spawnPlayerUseCase;
    }

    public Task Execute(CancellationToken cancellationToken)
    {
        _spawnPlayerUseCase.Execute();
        return Task.CompletedTask;
    }
}