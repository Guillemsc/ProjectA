using Game.GameContext.General.Configurations;
using Game.GameContext.PlayerKillers.Datas;
using Game.GameContext.PlayerKillers.UseCases;
using Game.GameContext.Players.UseCases;
using GUtils.Di.Builder;
using GUtils.Loading.Services;
using GUtils.Tasks.Runners;

namespace Game.GameContext.PlayerKillers.Installers;

public static class GamePlayerKillersInstaller
{
    public static void InstallGamePlayerKillers(this IDiContainerBuilder builder)
    {
        builder.Bind<PlayerKillersData>().FromNew();
        
        builder.Bind<WhenPlayerCollidedWithPlayerKillerUseCase>()
            .FromFunction(c => new WhenPlayerCollidedWithPlayerKillerUseCase(
                c.Resolve<KillPlayerAndReloadUseCase>()
            ));
        
        builder.Bind<KillPlayerAndReloadUseCase>()
            .FromFunction(c => new KillPlayerAndReloadUseCase(
                c.Resolve<GameApplicationContextConfiguration>(),
                c.Resolve<PlayerKillersData>(),
                c.Resolve<IAsyncTaskRunner>(),
                c.Resolve<ILoadingService>(),
                c.Resolve<KillPlayerUseCase>()
            ));
    }
}