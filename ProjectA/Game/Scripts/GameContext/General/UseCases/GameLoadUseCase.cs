using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.AngryBlocks.UseCases;
using Game.GameContext.Cameras.UseCases;
using Game.GameContext.GameUi.UseCases;
using Game.GameContext.Letters.UseCases;
using Game.GameContext.Maps.UseCases;
using Game.GameContext.Players.UseCases;
using Game.GameContext.Saves.UseCases;

namespace Game.GameContext.General.UseCases;

public sealed class GameLoadUseCase
{
    readonly LoadGameMapSaveDataUseCase _loadGameMapSaveDataUseCase;
    readonly SpawnMapUseCase _spawnMapUseCase;
    readonly SpawnPlayerUseCase _spawnPlayerUseCase;
    readonly SetupCameraUseCase _setupCameraUseCase;
    readonly SetCameraMapBoundsUseCase _setCameraMapBoundsUseCase;
    readonly SetInitialCameraAreaUseCase _setInitialCameraAreaUseCase;
    readonly SetPlayerAsCameraTargetUseCase _setPlayerAsCameraTargetUseCase;
    readonly LoadAngryBlocksUseCase _loadAngryBlocksUseCase;
    readonly LoadLettersUseCase _loadLettersUseCase;
    readonly ShowGameUiUseCase _showGameUiUseCase;

    public GameLoadUseCase(
        LoadGameMapSaveDataUseCase loadGameMapSaveDataUseCase,
        SpawnMapUseCase spawnMapUseCase,
        SpawnPlayerUseCase spawnPlayerUseCase, 
        SetupCameraUseCase setupCameraUseCase,
        SetCameraMapBoundsUseCase setCameraMapBoundsUseCase,
        SetInitialCameraAreaUseCase setInitialCameraAreaUseCase,
        SetPlayerAsCameraTargetUseCase setPlayerAsCameraTargetUseCase, 
        LoadAngryBlocksUseCase loadAngryBlocksUseCase,
        LoadLettersUseCase loadLettersUseCase,
        ShowGameUiUseCase showGameUiUseCase)
    {
        _loadGameMapSaveDataUseCase = loadGameMapSaveDataUseCase;
        _spawnMapUseCase = spawnMapUseCase;
        _spawnPlayerUseCase = spawnPlayerUseCase;
        _setupCameraUseCase = setupCameraUseCase;
        _setCameraMapBoundsUseCase = setCameraMapBoundsUseCase;
        _setPlayerAsCameraTargetUseCase = setPlayerAsCameraTargetUseCase;
        _showGameUiUseCase = showGameUiUseCase;
        _loadAngryBlocksUseCase = loadAngryBlocksUseCase;
        _loadLettersUseCase = loadLettersUseCase;
        _setInitialCameraAreaUseCase = setInitialCameraAreaUseCase;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        _loadGameMapSaveDataUseCase.Execute();
        await _spawnMapUseCase.Execute();
        _spawnPlayerUseCase.Execute();
        _setupCameraUseCase.Execute();
        _setCameraMapBoundsUseCase.Execute();
        _setInitialCameraAreaUseCase.Execute();
        _setPlayerAsCameraTargetUseCase.Execute(true);
        _loadAngryBlocksUseCase.Execute();
        _loadLettersUseCase.Execute();
        _showGameUiUseCase.Execute();
    }
}