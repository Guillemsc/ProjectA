using Game.GameContext.Connections.Datas;
using Game.GameContext.Connections.UseCases;
using Game.GameContext.General.Configurations;
using Game.GameContext.Maps.Datas;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.UseCases;
using Game.ServicesContext.LoadingScreen.Services;
using GUtils.Di.Builder;
using GUtils.Loading.Services;
using GUtils.Tick.Extensions;

namespace Game.GameContext.Connections.Installers;

public static class GameConnectionsInstaller
{
    public static void InstallGameConnections(this IDiContainerBuilder builder)
    {
        builder.Bind<ConnectionsData>().FromNew();
        
        builder.Bind<GetConnectionWithIdUseCase>()
            .FromFunction(c => new GetConnectionWithIdUseCase(
                c.Resolve<MapViewData>()
            ));

        builder.Bind<GetCurrentPlayerConnectionUseCase>()
            .FromFunction(c => new GetCurrentPlayerConnectionUseCase(
                c.Resolve<MapViewData>(),
                c.Resolve<PlayerViewData>()
            ));

        builder.Bind<RefreshCurrentPlayerConnectionUseCase>()
            .FromFunction(c => new RefreshCurrentPlayerConnectionUseCase(
                c.Resolve<ConnectionsData>(),
                c.Resolve<GetCurrentPlayerConnectionUseCase>(),
                c.Resolve<WhenCurrentPlayerConnectionChangedUseCase>()
            ))
            .LinkToTickablesService(o => o.Execute);
        
        builder.Bind<WhenCurrentPlayerConnectionChangedUseCase>()
            .FromFunction(c => new WhenCurrentPlayerConnectionChangedUseCase(
                c.Resolve<GameApplicationContextConfiguration>(),
                c.Resolve<ILoadingService>(),
                c.Resolve<ILoadingScreenService>(),
                c.Resolve<PlayerViewData>(),
                c.Resolve<FreezePlayerUseCase>()
            ));
    }
}