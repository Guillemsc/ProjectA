using Game.GameContext.PlayerKillers.UseCases;
using Game.GameContext.Players.UseCases;
using GUtils.Di.Builder;

namespace Game.GameContext.PlayerKillers.Installers;

public static class GamePlayerKillersInstaller
{
    public static void InstallGamePlayerKillers(this IDiContainerBuilder builder)
    {
        builder.Bind<WhenPlayerCollidedWithPlayerKillerUseCase>()
            .FromFunction(c => new WhenPlayerCollidedWithPlayerKillerUseCase(
                c.Resolve<KillPlayerUseCase>()
            ));
    }
}