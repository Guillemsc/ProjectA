using Game.GameContext.Connections.Datas;
using Game.GameContext.Connections.Views;
using Game.GameContext.General.ApplicationContexts;
using Game.GameContext.General.Configurations;
using Game.GameContext.Players.UseCases;
using Godot;
using GUtils.Loading.Extensions;
using GUtils.Loading.Services;

namespace Game.GameContext.Connections.UseCases;

public sealed class WhenCurrentPlayerConnectionChangedUseCase
{
    readonly GameApplicationContextConfiguration _contextConfiguration;
    readonly ILoadingService _loadingService;
    readonly ConnectionsData _connectionsData;
    readonly FreezePlayerUseCase _freezePlayerUseCase;

    public WhenCurrentPlayerConnectionChangedUseCase(
        GameApplicationContextConfiguration contextConfiguration,
        ILoadingService loadingService, 
        ConnectionsData connectionsData,
        FreezePlayerUseCase freezePlayerUseCase
        )
    {
        _contextConfiguration = contextConfiguration;
        _loadingService = loadingService;
        _connectionsData = connectionsData;
        _freezePlayerUseCase = freezePlayerUseCase;
    }

    public void Execute(ConnectionView connectionView)
    {
        if (!_connectionsData.HasIgnoredFirstConnectionCollision)
        {
            bool isInitialSpawn = _contextConfiguration.SpawnId.Equals(connectionView.SpawnId);

            if (isInitialSpawn)
            {
                _connectionsData.HasIgnoredFirstConnectionCollision = true;
                return;
            }
        }
        
        _freezePlayerUseCase.Execute();
        
        GameApplicationContextConfiguration contextConfiguration = new(connectionView.Map!, connectionView.SpawnId!);
        
        _loadingService.New()
            .EnqueueUnloadApplicationContext<GameApplicationContext>()
            .EnqueueLoadAndStartApplicationContext(new GameApplicationContext(contextConfiguration))
            .ExecuteAsync();
    }
}