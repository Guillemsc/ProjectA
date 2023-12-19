using Game.LoadingScreenContext.General.Interactors;
using Game.LoadingScreenContext.General.UseCases;
using Game.LoadingScreenContext.LoadingScreenUi.Interactors;
using Game.LoadingScreenContext.MapTransitionUi.Interactors;
using Game.ServicesContext.LoadingScreen.Services;
using GUtils.Di.Builder;
using GUtils.Loading.Services;
using GUtilsGodot.UiStack.Services;

namespace Game.LoadingScreenContext.General.Installers;

public static class LoadingScreenGeneralInstaller
{
    public static void InstallLoadingScreenGeneral(this IDiContainerBuilder builder)
    {
        builder.Bind<ILoadingScreenContextInteractor>()
            .FromFunction(c => new LoadingScreenContextInteractor(
                c.Resolve<LoadingScreenLoadUseCase>()
            ));

        builder.Bind<LoadingScreenLoadUseCase>()
            .FromFunction(c => new LoadingScreenLoadUseCase(
                c.Resolve<LinkToLoadingServiceUseCase>()
            ));

        builder.Bind<LinkToLoadingServiceUseCase>()
            .FromFunction(c => new LinkToLoadingServiceUseCase(
                c.Resolve<ILoadingService>(),
                c.Resolve<IUiStackService>(),
                c.Resolve<ILoadingScreenService>(),
                c.Resolve<ILoadingScreenUiInteractor>(),
                c.Resolve<IMapTransitionUiInteractor>()
            ));
    }
}