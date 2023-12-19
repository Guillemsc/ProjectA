using Game.ServicesContext.LoadingScreen.Services;
using GUtils.Di.Builder;
using GUtils.Loading.Services;
using GUtils.Services.Extensions;
using GUtils.Tick.Services;
using GUtils.Time.Services;
using GUtilsGodot.Cameras.Services;
using GUtilsGodot.UiStack.Services;

namespace Game.GameContext.General.Installers;

public static class GameGeneralServicesInstaller
{
    public static void InstallGameGeneralServices(this IDiContainerBuilder builder)
    {
        builder.Bind<IUiStackService>().FromServiceLocator();
        builder.Bind<ILoadingService>().FromServiceLocator();
        builder.Bind<ITickablesService>().FromServiceLocator();
        builder.Bind<IDeltaTimeService>().FromServiceLocator();
        builder.Bind<ICameras2dService>().FromServiceLocator();
        builder.Bind<ILoadingScreenService>().FromServiceLocator();
    }
}