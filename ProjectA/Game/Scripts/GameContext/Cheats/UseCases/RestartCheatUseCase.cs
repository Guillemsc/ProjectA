using Game.GameContext.General.ApplicationContexts;
using Game.GameContext.General.Configurations;
using GUtils.Loading.Extensions;
using GUtils.Loading.Services;

namespace Game.GameContext.Cheats.UseCases;

public sealed class RestartCheatUseCase
{
    readonly ILoadingService _loadingService;
    readonly GameApplicationContextConfiguration _contextConfiguration;
    
    public RestartCheatUseCase(
        ILoadingService loadingService, 
        GameApplicationContextConfiguration contextConfiguration
        )
    {
        _loadingService = loadingService;
        _contextConfiguration = contextConfiguration;
    }

    public void Execute()
    {
        if(_loadingService.IsLoading)
        {
            return;
        }
        
        _loadingService.New()
            .EnqueueUnloadApplicationContext<GameApplicationContext>()
            .EnqueueLoadAndStartApplicationContext(new GameApplicationContext(_contextConfiguration))
            .ExecuteAsync();
    }
}