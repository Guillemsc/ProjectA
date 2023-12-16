using Game.GameContext.Cheats.UseCases;
using GUtils.Di.Builder;
using GUtils.Loading.Services;
using GUtils.Tick.Extensions;

namespace Game.GameContext.Cheats.Installers;

public static class GameCheatsInstaller
{
    public static void InstallGameCheats(this IDiContainerBuilder builder)
    {
        builder.Bind<TickCheatsInputUseCase>()
            .FromFunction(c => new TickCheatsInputUseCase(
                c.Resolve<RestartCheatUseCase>()
                ))
            .LinkToTickablesService(o => o.Execute);

        builder.Bind<RestartCheatUseCase>()
            .FromFunction(c => new RestartCheatUseCase(
                c.Resolve<ILoadingService>()
            ));
    }
}