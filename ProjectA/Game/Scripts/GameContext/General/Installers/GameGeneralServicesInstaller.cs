using GUtils.Di.Builder;
using GUtils.Loading.Services;
using GUtils.Services.Extensions;
using GUtils.Tick.Services;
using GUtils.Time.Services;
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
    }
}