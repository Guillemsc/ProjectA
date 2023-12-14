using Game.Contexts.Configuration;
using Game.GameContext.General.Configurations;
using Game.GameContext.General.Installers;
using Game.GameContext.General.Interactors;
using Game.GameContext.Maps.Installers;
using Game.GameContext.Players.Installers;
using GUtils.ApplicationContexts.Contexts;
using GUtils.Di.Contexts;
using GUtils.Di.Installers;
using GUtils.Services.Locators;
using GUtilsGodot.Di.Loadables;
using GUtilsGodot.Roots.Services;

namespace Game.GameContext.General.ApplicationContexts;

public sealed class GameApplicationContext : DiApplicationContext<IGameContextInteractor>
{
    readonly GameConfiguration _gameConfiguration;

    public GameApplicationContext(GameConfiguration gameConfiguration)
    {
        _gameConfiguration = gameConfiguration;
    }

    protected override void Install(IDiContext<IGameContextInteractor> context)
    {
        ContextsScenesConfiguration contextsScenesConfiguration = ServiceLocator.Get<ContextsScenesConfiguration>();
        IRootNodeService rootNodeService = ServiceLocator.Get<IRootNodeService>();
        
        context.AddInstallerLoadable(new PackedSceneNodeInstallerLoadable(contextsScenesConfiguration.GameContextPrefab!, rootNodeService.Root));
        
        context.AddInstaller(new CallbackInstaller(
            b =>
            {
                b.InstallGameGeneralConfigurations(_gameConfiguration);
                b.InstallGameGeneralServices();
                b.InstallGameGeneral();
                b.InstallGamePlayers();
                b.InstallGameMaps();
            }));
    }
}