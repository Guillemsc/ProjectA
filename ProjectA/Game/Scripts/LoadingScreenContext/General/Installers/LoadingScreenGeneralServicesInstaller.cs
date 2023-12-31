using Game.ServicesContext.LoadingScreen.Services;
using GUtils.Di.Builder;
using GUtils.Loading.Services;
using GUtils.Services.Extensions;
using GUtilsGodot.UiStack.Services;

namespace Game.LoadingScreenContext.General.Installers;

public static class LoadingScreenGeneralServicesInstaller
{
    public static void InstallLoadingScreenGeneralServices(this IDiContainerBuilder builder)
    {
        builder.Bind<IUiStackService>().FromServiceLocator();
        builder.Bind<ILoadingService>().FromServiceLocator();
        builder.Bind<ILoadingScreenService>().FromServiceLocator();
    }
}