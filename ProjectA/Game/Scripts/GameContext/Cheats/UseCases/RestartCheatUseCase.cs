using Game.GameContext.General.ApplicationContexts;
using GUtils.Loading.Extensions;
using GUtils.Loading.Services;

namespace Game.GameContext.Cheats.UseCases;

public sealed class RestartCheatUseCase
{
    readonly ILoadingService _loadingService;

    public RestartCheatUseCase(ILoadingService loadingService)
    {
        _loadingService = loadingService;
    }

    public void Execute()
    {
        if(_loadingService.IsLoading)
        {
            return;
        }
        
        _loadingService.New()
            .EnqueueUnloadApplicationContext<GameApplicationContext>()
            .EnqueueLoadAndStartApplicationContext(new GameApplicationContext())
            .ExecuteAsync();
    }
}