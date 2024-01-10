using Game.GameContext.Outro.UseCases;
using Game.GameContext.OutroUi.Interactors;
using GUtils.Di.Builder;
using GUtils.Loading.Services;

namespace Game.GameContext.Outro.Installers;

public static class GameOutroInstaller
{
    public static void InstallGameOutro(this IDiContainerBuilder builder)
    {
        builder.Bind<GoToMainMenuAfterOutroUseCase>()
            .FromFunction(c => new GoToMainMenuAfterOutroUseCase(
                c.Resolve<ILoadingService>()
            ));
        
        builder.Bind<PlayOutroUseCase>()
            .FromFunction(c => new PlayOutroUseCase(
                c.Resolve<IOutroUiInteractor>(),
                c.Resolve<GoToMainMenuAfterOutroUseCase>()
            ));
    }
}