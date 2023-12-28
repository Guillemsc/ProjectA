using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Cameras.UseCases;
using Game.GameContext.GameUi.UseCases;
using Game.GameContext.Maps.UseCases;
using Game.GameContext.Players.UseCases;

namespace Game.GameContext.General.UseCases;

public sealed class GameLoadUseCase
{
    readonly SpawnMapUseCase _spawnMapUseCase;
    readonly SpawnPlayerUseCase _spawnPlayerUseCase;
    readonly SetupCameraUseCase _setupCameraUseCase;
    readonly SetCameraMapBoundsUseCase _setCameraMapBoundsUseCase;
    readonly SetInitialCameraAreaUseCase _setInitialCameraAreaUseCase;
    readonly SetPlayerAsCameraTargetUseCase _setPlayerAsCameraTargetUseCase;
    readonly ShowGameUiUseCase _showGameUiUseCase;

    public GameLoadUseCase(
        SpawnMapUseCase spawnMapUseCase,
        SpawnPlayerUseCase spawnPlayerUseCase, 
        SetupCameraUseCase setupCameraUseCase,
        SetCameraMapBoundsUseCase setCameraMapBoundsUseCase,
        SetInitialCameraAreaUseCase setInitialCameraAreaUseCase,
        SetPlayerAsCameraTargetUseCase setPlayerAsCameraTargetUseCase, 
        ShowGameUiUseCase showGameUiUseCase
        )
    {
        _spawnMapUseCase = spawnMapUseCase;
        _spawnPlayerUseCase = spawnPlayerUseCase;
        _setupCameraUseCase = setupCameraUseCase;
        _setCameraMapBoundsUseCase = setCameraMapBoundsUseCase;
        _setPlayerAsCameraTargetUseCase = setPlayerAsCameraTargetUseCase;
        _showGameUiUseCase = showGameUiUseCase;
        _setInitialCameraAreaUseCase = setInitialCameraAreaUseCase;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        await _spawnMapUseCase.Execute();
        _spawnPlayerUseCase.Execute();
        _setupCameraUseCase.Execute();
        _setCameraMapBoundsUseCase.Execute();
        _setInitialCameraAreaUseCase.Execute();
        _setPlayerAsCameraTargetUseCase.Execute();
        _showGameUiUseCase.Execute();
    }
}