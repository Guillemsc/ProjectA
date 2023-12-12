using GUtils.ApplicationContexts.Services;
using GUtils.Di.Builder;
using GUtils.Loading.Services;
using GUtils.Services.Extensions;
using GUtilsGodot.Di.Installers;
using GUtilsGodot.Roots.Services;

namespace Game.ServicesContext.General.Installers;

public partial class ServicesContextNodeInstaller : NodeInstaller
{
    public override void Install(IDiContainerBuilder builder)
    {
        builder.Bind<ILoadingService>()
            .FromFunction(c => new LoadingService())
            .LinkToServiceLocator();

        builder.Bind<IApplicationContextService>()
            .FromFunction(c => new ApplicationContextService())
            .LinkToServiceLocator();
        
        builder.Bind<IRootNodeService>()
            .FromFunction(c => new RootNodeService(GetParent()))
            .LinkToServiceLocator();;
    }
}