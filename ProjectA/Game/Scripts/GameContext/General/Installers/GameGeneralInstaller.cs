using Game.GameContext.AngryBlocks.UseCases;
using Game.GameContext.Cameras.UseCases;
using Game.GameContext.Cinematics.UseCases;
using Game.GameContext.GameUi.UseCases;
using Game.GameContext.General.Interactors;
using Game.GameContext.General.UseCases;
using Game.GameContext.Maps.UseCases;
using Game.GameContext.Players.UseCases;
using GUtils.Di.Builder;
using GUtils.Extensions;
using GUtils.Randomization.Generators;
using GUtils.Tasks.Extensions;
using GUtils.Tasks.Runners;
using GUtils.Tasks.Trackers;

namespace Game.GameContext.General.Installers;

public static class GameGeneralInstaller
{
    public static void InstallGameGeneral(this IDiContainerBuilder builder)
    {
        builder.Bind<IGameContextInteractor>()
            .FromFunction(c => new GameContextInteractor(
                c.Resolve<GameLoadUseCase>(),
                c.Resolve<GameStartUseCase>()
            ));

        builder.Bind<GameLoadUseCase>()
            .FromFunction(c => new GameLoadUseCase(
                c.Resolve<SpawnMapUseCase>(),
                c.Resolve<SpawnPlayerUseCase>(),
                c.Resolve<SetupCameraUseCase>(),
                c.Resolve<SetCameraMapBoundsUseCase>(),
                c.Resolve<SetInitialCameraAreaUseCase>(),
                c.Resolve<SetPlayerAsCameraTargetUseCase>(),
                c.Resolve<LoadAngryBlocksUseCase>(),
                c.Resolve<ShowGameUiUseCase>()
            ));

        builder.Bind<GameStartUseCase>()
            .FromFunction(c => new GameStartUseCase(
                c.Resolve<StartPlayerUseCase>(),
                c.Resolve<TryPlayStartingMapCinematicUseCase>()
            ));

        builder.InstallAsyncTaskRunner();

        builder.Bind<IRandomGenerator>()
            .FromInstance(new SeedRandomGenerator(0));
    }
}