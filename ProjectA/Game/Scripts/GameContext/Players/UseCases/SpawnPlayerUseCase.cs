using Game.GameContext.General.Datas;
using Game.GameContext.Players.Configurations;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using GUtils.Locations.Enums;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Players.UseCases;

public sealed class SpawnPlayerUseCase
{
    readonly GamePlayersConfiguration _gamePlayersConfiguration;
    readonly PlayerViewData _playerViewData;
    readonly GameGeneralViewData _gameGeneralViewData;
    readonly WhenPlayerStartedCollisionWithWallUseCase _whenPlayerStartedCollisionWithWallUseCase;
    readonly WhenPlayerStoppedCollisionWithWallUseCase _whenPlayerStoppedCollisionWithWallUseCase;
    readonly WhenPlayerStartedCollisionWithInteractionUseCase _whenPlayerStartedCollisionWithInteractionUseCase;

    public SpawnPlayerUseCase(
        GamePlayersConfiguration gamePlayersConfiguration, 
        PlayerViewData playerViewData, 
        GameGeneralViewData gameGeneralViewData, 
        WhenPlayerStartedCollisionWithWallUseCase whenPlayerStartedCollisionWithWallUseCase,
        WhenPlayerStoppedCollisionWithWallUseCase whenPlayerStoppedCollisionWithWallUseCase, 
        WhenPlayerStartedCollisionWithInteractionUseCase whenPlayerStartedCollisionWithInteractionUseCase
        )
    {
        _gamePlayersConfiguration = gamePlayersConfiguration;
        _playerViewData = playerViewData;
        _gameGeneralViewData = gameGeneralViewData;
        _whenPlayerStartedCollisionWithWallUseCase = whenPlayerStartedCollisionWithWallUseCase;
        _whenPlayerStoppedCollisionWithWallUseCase = whenPlayerStoppedCollisionWithWallUseCase;
        _whenPlayerStartedCollisionWithInteractionUseCase = whenPlayerStartedCollisionWithInteractionUseCase;
    }

    public void Execute()
    {
        PlayerView playerView = _gamePlayersConfiguration.PlayerPrefab!.Instantiate<PlayerView>();
        playerView.SetParent(_gameGeneralViewData.Root);
        
        playerView.LeftWallDetector!.ConnectBodyEntered(_ => _whenPlayerStartedCollisionWithWallUseCase.Execute(HorizontalLocation.Left));
        playerView.RightWallDetector!.ConnectBodyEntered(_ => _whenPlayerStartedCollisionWithWallUseCase.Execute(HorizontalLocation.Right));
        
        playerView.LeftWallDetector!.ConnectBodyExited(_ => _whenPlayerStoppedCollisionWithWallUseCase.Execute());
        playerView.RightWallDetector!.ConnectBodyExited(_ => _whenPlayerStoppedCollisionWithWallUseCase.Execute());
        
        playerView.InteractionsDetector!.ConnectAreaEntered(_whenPlayerStartedCollisionWithInteractionUseCase.Execute);
        
        _playerViewData.PlayerView = playerView;
    }
}