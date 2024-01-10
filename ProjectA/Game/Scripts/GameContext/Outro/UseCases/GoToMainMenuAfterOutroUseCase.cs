using Game.GameContext.General.ApplicationContexts;
using Game.MetaContext.General.ApplicationContexts;
using Game.MetaContext.General.Configurations;
using GUtils.Loading.Extensions;
using GUtils.Loading.Services;

namespace Game.GameContext.Outro.UseCases;

public sealed class GoToMainMenuAfterOutroUseCase
{
    readonly ILoadingService _loadingService;

    public GoToMainMenuAfterOutroUseCase(
        ILoadingService loadingService
        )
    {
        _loadingService = loadingService;
    }

    public void Execute()
    {
        MetaApplicationContextConfiguration contextConfiguration = new(false);
        
        _loadingService.New()
            .EnqueueUnloadApplicationContext<GameApplicationContext>()
            .EnqueueLoadAndStartApplicationContext(new MetaApplicationContext(contextConfiguration))
            .ExecuteAsync();
    }
}