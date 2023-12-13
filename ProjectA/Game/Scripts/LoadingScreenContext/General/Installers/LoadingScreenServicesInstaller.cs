using GUtils.Di.Builder;
using GUtils.Loading.Services;
using GUtils.Services.Extensions;
using GUtilsGodot.UiStack.Services;

namespace Game.LoadingScreenContext.General.Installers;

public static class LoadingScreenServicesInstaller
{
    public static void InstallLoadingScreenGeneralServices(this IDiContainerBuilder builder)
    {
        builder.Bind<IUiStackService>().FromServiceLocator();
        builder.Bind<ILoadingService>().FromServiceLocator();
    }
}