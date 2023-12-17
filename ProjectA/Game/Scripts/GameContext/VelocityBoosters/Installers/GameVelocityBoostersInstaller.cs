using Game.GameContext.Players.Datas;
using Game.GameContext.VelocityBoosters.UseCases;
using GUtils.Di.Builder;

namespace Game.GameContext.VelocityBoosters.Installers;

public static class GameVelocityBoostersInstaller
{
    public static void InstallGameVelocityBoosters(this IDiContainerBuilder builder)
    {
        builder.Bind<WhenPlayerCollidedWithVelocityBoosterUseCase>()
            .FromFunction(c => new WhenPlayerCollidedWithVelocityBoosterUseCase(
                c.Resolve<PlayerViewData>()
            ));
    }
}