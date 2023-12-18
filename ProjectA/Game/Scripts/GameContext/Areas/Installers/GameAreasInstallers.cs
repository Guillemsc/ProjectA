using Game.GameContext.Areas.Datas;
using Game.GameContext.Areas.UseCases;
using Game.GameContext.Cameras.UseCases;
using Game.GameContext.Maps.Datas;
using Game.GameContext.Players.Datas;
using GUtils.Di.Builder;
using GUtils.Tick.Extensions;

namespace Game.GameContext.Areas.Installers;

public static class GameAreasInstallers
{
    public static void InstallGameAreas(this IDiContainerBuilder builder)
    {
        builder.Bind<AreasData>().FromNew();

        builder.Bind<GetCurrentPlayerAreaUseCase>()
            .FromFunction(c => new GetCurrentPlayerAreaUseCase(
                c.Resolve<PlayerViewData>(),
                c.Resolve<MapViewData>()
            ));
        
        builder.Bind<RefreshCurrentPlayerAreaUseCase>()
            .FromFunction(c => new RefreshCurrentPlayerAreaUseCase(
                c.Resolve<AreasData>(),
                c.Resolve<GetCurrentPlayerAreaUseCase>(),
                c.Resolve<WhenCurrentPlayerAreaChangesUseCase>()
            ))
            .LinkToTickablesService(o => o.Execute);

        builder.Bind<WhenCurrentPlayerAreaChangesUseCase>()
            .FromFunction(c => new WhenCurrentPlayerAreaChangesUseCase(
                c.Resolve<SetCameraAreaUseCase>()

            ));
    }
}