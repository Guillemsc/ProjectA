using Game.LoadingScreenContext.General.Interactors;
using GUtils.Di.Builder;

namespace Game.LoadingScreenContext.General.Installers;

public static class LoadingScreenContextInteractorInstaller
{
    public static void InstallLoadingScreenGeneralInteractor(this IDiContainerBuilder builder)
    {
        builder.Bind<ILoadingScreenContextInteractor>()
            .FromFunction(c => new LoadingScreenContextInteractor(
            ));
    }
}