using Game.GameContext.Connections.Datas;
using Game.GameContext.Connections.Views;
using Game.GameContext.General.ApplicationContexts;
using Game.GameContext.General.Configurations;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.UseCases;
using Game.GameContext.Players.Views;
using Game.ServicesContext.LoadingScreen.Enums;
using Game.ServicesContext.LoadingScreen.Services;
using Godot;
using GUtils.Directions;
using GUtils.Loading.Extensions;
using GUtils.Loading.Services;

namespace Game.GameContext.Connections.UseCases;

public sealed class WhenCurrentPlayerConnectionChangedUseCase
{
    readonly GameApplicationContextConfiguration _contextConfiguration;
    readonly ILoadingService _loadingService;
    readonly ILoadingScreenService _loadingScreenService;
    readonly PlayerViewData _playerViewData;
    readonly FreezePlayerUseCase _freezePlayerUseCase;

    public WhenCurrentPlayerConnectionChangedUseCase(
        GameApplicationContextConfiguration contextConfiguration,
        ILoadingService loadingService, 
        ILoadingScreenService loadingScreenService,
        PlayerViewData playerViewData,
        FreezePlayerUseCase freezePlayerUseCase
        )
    {
        _contextConfiguration = contextConfiguration;
        _loadingService = loadingService;
        _loadingScreenService = loadingScreenService;
        _playerViewData = playerViewData;
        _freezePlayerUseCase = freezePlayerUseCase;
    }

    public void Execute(ConnectionView connectionView)
    {
        HorizontalDirection playerDirection = HorizontalDirection.Right;
        
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (hasPlayer)
        {
            playerDirection = playerView.AnimationPlayer!.HorizontalDirection;
        }
        
        _freezePlayerUseCase.Execute();

        GameApplicationContextConfiguration contextConfiguration = new(
            connectionView.Map!,
            connectionView.SpawnId!,
            false,
            playerDirection
        );

        _loadingScreenService.LoadingScreenToUse = LoadingScreenType.MapTransition;
        
        _loadingService.New()
            .EnqueueUnloadApplicationContext<GameApplicationContext>()
            .EnqueueLoadAndStartApplicationContext(new GameApplicationContext(contextConfiguration))
            .EnqueueGCCollect()
            .ExecuteAsync();
    }
}