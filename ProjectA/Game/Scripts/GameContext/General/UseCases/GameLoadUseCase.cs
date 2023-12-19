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
    readonly SetInitialCameraAreaUseCase _setInitialCameraAreaUseCase;
    readonly SetPlayerAsCameraTargetUseCase _setPlayerAsCameraTargetUseCase;

    public GameLoadUseCase(
        SpawnMapUseCase spawnMapUseCase,
        SpawnPlayerUseCase spawnPlayerUseCase, 
        SetupCameraUseCase setupCameraUseCase,
        SetInitialCameraAreaUseCase setInitialCameraAreaUseCase,
        SetPlayerAsCameraTargetUseCase setPlayerAsCameraTargetUseCase
        )
    {
        _spawnMapUseCase = spawnMapUseCase;
        _spawnPlayerUseCase = spawnPlayerUseCase;
        _setPlayerAsCameraTargetUseCase = setPlayerAsCameraTargetUseCase;
        _setInitialCameraAreaUseCase = setInitialCameraAreaUseCase;
        _setupCameraUseCase = setupCameraUseCase;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        await _spawnMapUseCase.Execute();
        _spawnPlayerUseCase.Execute();
        _setupCameraUseCase.Execute();
        _setInitialCameraAreaUseCase.Execute();
        _setPlayerAsCameraTargetUseCase.Execute();
    }
}