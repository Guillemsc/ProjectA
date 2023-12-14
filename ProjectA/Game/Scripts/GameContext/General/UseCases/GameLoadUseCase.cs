using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Maps.UseCases;
using Game.GameContext.Players.UseCases;

namespace Game.GameContext.General.UseCases;

public sealed class GameLoadUseCase
{
    readonly SpawnMapUseCase _spawnMapUseCase;
    readonly SpawnPlayerUseCase _spawnPlayerUseCase;

    public GameLoadUseCase(
        SpawnMapUseCase spawnMapUseCase,
        SpawnPlayerUseCase spawnPlayerUseCase
        )
    {
        _spawnMapUseCase = spawnMapUseCase;
        _spawnPlayerUseCase = spawnPlayerUseCase;
    }

    public Task Execute(CancellationToken cancellationToken)
    {
        _spawnMapUseCase.Execute();
        _spawnPlayerUseCase.Execute();
        return Task.CompletedTask;
    }
}