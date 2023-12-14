using Game.GameContext.General.Datas;
using Game.GameContext.Player.Configurations;
using Game.GameContext.Player.Datas;
using Game.GameContext.Player.Views;
using GUtils.Locations.Enums;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Player.UseCases;

public sealed class SpawnPlayerUseCase
{
    readonly GamePlayerConfiguration _gamePlayerConfiguration;
    readonly PlayerViewData _playerViewData;
    readonly GameGeneralViewData _gameGeneralViewData;
    readonly WhenPlayerStartedCollisionWithWallUseCase _whenPlayerStartedCollisionWithWallUseCase;
    readonly WhenPlayerStoppedCollisionWithWallUseCase _whenPlayerStoppedCollisionWithWallUseCase;

    public SpawnPlayerUseCase(
        GamePlayerConfiguration gamePlayerConfiguration, 
        PlayerViewData playerViewData, 
        GameGeneralViewData gameGeneralViewData, 
        WhenPlayerStartedCollisionWithWallUseCase whenPlayerStartedCollisionWithWallUseCase,
        WhenPlayerStoppedCollisionWithWallUseCase whenPlayerStoppedCollisionWithWallUseCase
        )
    {
        _gamePlayerConfiguration = gamePlayerConfiguration;
        _playerViewData = playerViewData;
        _gameGeneralViewData = gameGeneralViewData;
        _whenPlayerStartedCollisionWithWallUseCase = whenPlayerStartedCollisionWithWallUseCase;
        _whenPlayerStoppedCollisionWithWallUseCase = whenPlayerStoppedCollisionWithWallUseCase;
    }

    public void Execute()
    {
        PlayerView playerView = _gamePlayerConfiguration.PlayerPrefab!.Instantiate<PlayerView>();
        
        playerView.LeftWallDetector!.ConnectBodyEntered(_ => _whenPlayerStartedCollisionWithWallUseCase.Execute(HorizontalLocation.Left));
        playerView.RightWallDetector!.ConnectBodyEntered(_ => _whenPlayerStartedCollisionWithWallUseCase.Execute(HorizontalLocation.Right));
        
        playerView.LeftWallDetector!.ConnectBodyExited(_ => _whenPlayerStoppedCollisionWithWallUseCase.Execute());
        playerView.RightWallDetector!.ConnectBodyExited(_ => _whenPlayerStoppedCollisionWithWallUseCase.Execute());
        
        _playerViewData.PlayerView = playerView;
        playerView.SetParent(_gameGeneralViewData.PlayerParent);
    }
}