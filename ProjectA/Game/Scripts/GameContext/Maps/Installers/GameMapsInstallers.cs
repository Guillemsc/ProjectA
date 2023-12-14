using Game.GameContext.General.Datas;
using Game.GameContext.Maps.Configurations;
using Game.GameContext.Maps.Datas;
using Game.GameContext.Maps.UseCases;
using GUtils.Di.Builder;

namespace Game.GameContext.Maps.Installers;

public static class GameMapsInstallers
{
    public static void InstallGameMaps(this IDiContainerBuilder builder)
    {
        builder.Bind<MapViewData>().FromNew();

        builder.Bind<SpawnMapUseCase>()
            .FromFunction(c => new SpawnMapUseCase(
                c.Resolve<GameMapsConfiguration>(),
                c.Resolve<MapViewData>(),
                c.Resolve<GameGeneralViewData>()
            ));
    }
}