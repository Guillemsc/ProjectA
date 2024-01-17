using Game.GameContext.Pause.Configurations;
using Game.GameContext.Pause.Datas;
using Game.GameContext.Pause.UseCases;
using Game.GameContext.PauseUi.Interactors;
using Game.ServicesContext.Time.Services;
using GUtils.Di.Builder;
using GUtils.Pooling.Extensions;
using GUtils.Tasks.Runners;
using GUtils.Tick.Extensions;

namespace Game.GameContext.Pause.Installers;

public static class GamePauseInstaller
{
    public static void InstallGamePause(this IDiContainerBuilder builder)
    {
        builder.Bind<PauseData>().FromNew();
        
        builder.Bind<TickPauseInputUseCase>()
            .FromFunction(c => new TickPauseInputUseCase(
                c.Resolve<ToggleGamePauseUiUseCase>()
            ))
            .LinkExecutableToTickablesService();
        
        builder.Bind<ToggleGamePauseUiUseCase>()
            .FromFunction(c => new ToggleGamePauseUiUseCase(
                c.Resolve<PauseData>(),
                c.Resolve<IAsyncTaskRunner>(),
                c.Resolve<IPauseUiInteractor>(),
                c.Resolve<SetGameLogicPausedUseCase>()
            ));

        builder.Bind<SetGameLogicPausedUseCase>()
            .FromFunction(c => new SetGameLogicPausedUseCase(
                c.Resolve<IGameTimesService>(),
                c.Resolve<PauseData>()
            ));

        builder.Bind<PauseGameLogicSomeFramesUseCase>()
            .FromAutoPool(
                (c, o) => o.Init(
                    c.Resolve<GamePauseConfiguration>(),
                    c.Resolve<IAsyncTaskRunner>(),
                    c.Resolve<SetGameLogicPausedUseCase>()
                ));
    }
}