using Game.GameContext.Entities.Services;
using GUtils.Di.Builder;
using GUtils.Services.Extensions;

namespace Game.GameContext.Entities.Installers;

public static class GameEntitiesInstaller
{
    public static void InstallGameEntities(this IDiContainerBuilder builder)
    {
        builder.Bind<IGameEntitiesService>()
            .FromFunction(c => new GameEntitiesService())
            .LinkToServiceLocator();
    }
}