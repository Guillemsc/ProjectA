using Game.GameContext.Connections.Datas;
using Game.GameContext.Connections.Views;
using Game.GameContext.General.ApplicationContexts;
using Game.GameContext.General.Configurations;
using Game.GameContext.Players.UseCases;
using Game.ServicesContext.LoadingScreen.Enums;
using Game.ServicesContext.LoadingScreen.Services;
using Godot;
using GUtils.Loading.Extensions;
using GUtils.Loading.Services;

namespace Game.GameContext.Connections.UseCases;

public sealed class WhenCurrentPlayerConnectionChangedUseCase
{
    readonly GameApplicationContextConfiguration _contextConfiguration;
    readonly ILoadingService _loadingService;
    readonly ILoadingScreenService _loadingScreenService;
    readonly ConnectionsData _connectionsData;
    readonly FreezePlayerUseCase _freezePlayerUseCase;

    public WhenCurrentPlayerConnectionChangedUseCase(
        GameApplicationContextConfiguration contextConfiguration,
        ILoadingService loadingService, 
        ILoadingScreenService loadingScreenService,
        ConnectionsData connectionsData,
        FreezePlayerUseCase freezePlayerUseCase
        )
    {
        _contextConfiguration = contextConfiguration;
        _loadingService = loadingService;
        _loadingScreenService = loadingScreenService;
        _connectionsData = connectionsData;
        _freezePlayerUseCase = freezePlayerUseCase;
    }

    public void Execute(ConnectionView connectionView)
    {
        _freezePlayerUseCase.Execute();
        
        GameApplicationContextConfiguration contextConfiguration = new(connectionView.Map!, connectionView.SpawnId!, false);

        _loadingScreenService.LoadingScreenToUse = LoadingScreenType.MapTransition;
        
        _loadingService.New()
            .EnqueueUnloadApplicationContext<GameApplicationContext>()
            .EnqueueLoadAndStartApplicationContext(new GameApplicationContext(contextConfiguration))
            .ExecuteAsync();
    }
}