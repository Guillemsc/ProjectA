using Game.GameContext.General.Interactors;
using GUtils.Di.Builder;

namespace Game.GameContext.General.Installers;

public static class GameInteractorInstaller
{
    public static void InstallGameInteractors(this IDiContainerBuilder builder)
    {
        builder.Bind<IGameContextInteractor>()
            .FromFunction(c => new GameContextInteractor(
            ));
    }
}