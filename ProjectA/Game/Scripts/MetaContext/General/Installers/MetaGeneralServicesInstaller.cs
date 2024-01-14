using GUtils.Di.Builder;
using GUtils.Loading.Services;
using GUtils.Services.Extensions;
using GUtils.Tick.Services;
using GUtilsGodot.Quitting.Services;
using GUtilsGodot.UiStack.Services;

namespace Game.MetaContext.General.Installers;

public static class MetaGeneralServicesInstaller
{
    public static void InstallMetaGeneralServices(this IDiContainerBuilder builder)
    {
        builder.Bind<IUiStackService>().FromServiceLocator();
        builder.Bind<ILoadingService>().FromServiceLocator();
        builder.Bind<ITickablesService>().FromServiceLocator();
        builder.Bind<IQuitService>().FromServiceLocator();
    }
}