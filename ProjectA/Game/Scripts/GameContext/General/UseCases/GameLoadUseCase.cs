using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Cameras.UseCases;
using Game.GameContext.Maps.UseCases;
using Game.GameContext.Players.UseCases;

namespace Game.GameContext.General.UseCases;

public sealed class GameLoadUseCase
{
    readonly SpawnMapUseCase _spawnMapUseCase;
    readonly SpawnPlayerUseCase _spawnPlayerUseCase;
    readonly SetupCameraUseCase _setupCameraUseCase;
    readonly SetPlayerAsCameraTargetUseCase _setPlayerAsCameraTargetUseCase;

    public GameLoadUseCase(
        SpawnMapUseCase spawnMapUseCase,
        SpawnPlayerUseCase spawnPlayerUseCase, 
        SetupCameraUseCase setupCameraUseCase,
        SetPlayerAsCameraTargetUseCase setPlayerAsCameraTargetUseCase)
    {
        _spawnMapUseCase = spawnMapUseCase;
        _spawnPlayerUseCase = spawnPlayerUseCase;
        _setPlayerAsCameraTargetUseCase = setPlayerAsCameraTargetUseCase;
        _setupCameraUseCase = setupCameraUseCase;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        await _spawnMapUseCase.Execute();
        _spawnPlayerUseCase.Execute();
        _setupCameraUseCase.Execute();
        _setPlayerAsCameraTargetUseCase.Execute();
    }
}