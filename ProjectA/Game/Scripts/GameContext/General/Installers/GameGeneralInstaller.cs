using Game.GameContext.General.Interactors;
using Game.GameContext.General.UseCases;
using Game.GameContext.Maps.UseCases;
using Game.GameContext.Players.UseCases;
using GUtils.Di.Builder;
using GUtils.Extensions;
using GUtils.Tasks.Runners;
using GUtils.Tasks.Trackers;

namespace Game.GameContext.General.Installers;

public static class GameGeneralInstaller
{
    public static void InstallGameGeneral(this IDiContainerBuilder builder)
    {
        builder.Bind<IGameContextInteractor>()
            .FromFunction(c => new GameContextInteractor(
                c.Resolve<GameLoadUseCase>()
            ));

        builder.Bind<GameLoadUseCase>()
            .FromFunction(c => new GameLoadUseCase(
                c.Resolve<SpawnMapUseCase>(),
                c.Resolve<SpawnPlayerUseCase>()
            ));

        builder.Bind<IAsyncTaskRunner, AsyncTaskRunner>()
            .FromFunction(c => new AsyncTaskRunner())
            .WhenDispose(o => o.CancelForever)
            .LinkDisposable();
    }
}