using Game.ServicesContext.LoadingScreen.Services;
using Game.ServicesContext.Music.Services;
using Game.ServicesContext.Saves.Services;
using Game.ServicesContext.Time.Services;
using GUtils.Di.Builder;
using GUtils.Loading.Services;
using GUtils.Services.Extensions;
using GUtils.Tick.Services;
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
        builder.Bind<IGameTimesService>().FromServiceLocator();
        builder.Bind<ICameras2dService>().FromServiceLocator();
        builder.Bind<ILoadingScreenService>().FromServiceLocator();
        builder.Bind<IMusicService>().FromServiceLocator();
        builder.Bind<IGameSavesService>().FromServiceLocator();
    }
}