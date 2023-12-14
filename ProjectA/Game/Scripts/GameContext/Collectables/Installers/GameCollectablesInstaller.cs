using Game.GameContext.Collectables.UseCases;
using GUtils.Di.Builder;

namespace Game.GameContext.Collectables.Installers;

public static class GameCollectablesInstaller
{
    public static void InstallGameCollectables(this IDiContainerBuilder builder)
    {
        builder.Bind<CollectCollectableUseCase>()
            .FromFunction(c => new CollectCollectableUseCase(
                ));
    }
}