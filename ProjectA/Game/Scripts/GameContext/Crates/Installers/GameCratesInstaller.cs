using Game.GameContext.Crates.UseCases;
using Game.GameContext.Players.Datas;
using GUtils.Di.Builder;

namespace Game.GameContext.Crates.Installers;

public static class GameCratesInstaller
{
    public static void InstallGameCrates(this IDiContainerBuilder builder)
    {
        builder.Bind<WhenPlayerCollidedWithCrateUseCase>()
            .FromFunction(c => new WhenPlayerCollidedWithCrateUseCase(
                c.Resolve<PlayerViewData>()
            ));
    }
}