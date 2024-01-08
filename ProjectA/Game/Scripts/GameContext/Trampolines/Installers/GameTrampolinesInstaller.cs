using Game.GameContext.Cameras.UseCases;
using Game.GameContext.Players.Datas;
using Game.GameContext.Trampolines.UseCases;
using GUtils.Di.Builder;

namespace Game.GameContext.Trampolines.Installers;

public static class GameTrampolinesInstaller
{
    public static void InstallGameTrampolines(this IDiContainerBuilder builder)
    {
        builder.Bind<WhenPlayerCollidedWithTrampolineUseCase>()
            .FromFunction(c => new WhenPlayerCollidedWithTrampolineUseCase(
                c.Resolve<PlayerViewData>(),
                c.Resolve<ShakeCameraUseCase>()
            ));
    }
}