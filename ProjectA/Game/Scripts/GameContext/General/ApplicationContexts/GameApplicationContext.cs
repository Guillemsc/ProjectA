using Game.GameContext.Areas.Installers;
using Game.GameContext.Cameras.Installers;
using Game.GameContext.Cheats.Installers;
using Game.GameContext.Cinematics.Installers;
using Game.GameContext.Collectables.Installers;
using Game.GameContext.Connections.Installers;
using Game.GameContext.Crates.Installers;
using Game.GameContext.Dialogues.Installers;
using Game.GameContext.Fruits.Installers;
using Game.GameContext.General.Configurations;
using Game.GameContext.General.Installers;
using Game.GameContext.General.Interactors;
using Game.GameContext.Letters.Installers;
using Game.GameContext.Maps.Installers;
using Game.GameContext.Outro.Installers;
using Game.GameContext.Pause.Installers;
using Game.GameContext.PlayerKillers.Installers;
using Game.GameContext.Players.Installers;
using Game.GameContext.Trampolines.Installers;
using Game.GameContext.VelocityBoosters.Installers;
using Game.GameContext.VisualEffects.Installers;
using GUtils.ApplicationContexts.Contexts;
using GUtils.Di.Contexts;
using GUtils.Di.Installers;
using GUtils.Services.Locators;
using GUtilsGodot.Di.Loadables;
using GUtilsGodot.Roots.Services;
using ContextsScenesConfiguration = Game.General.Contexts.Configuration.ContextsScenesConfiguration;

namespace Game.GameContext.General.ApplicationContexts;

public sealed class GameApplicationContext : DiApplicationContext<IGameContextInteractor>
{
    readonly GameApplicationContextConfiguration _contextConfiguration;

    public GameApplicationContext(GameApplicationContextConfiguration contextConfiguration)
    {
        _contextConfiguration = contextConfiguration;
    }

    protected override void Install(IDiContext<IGameContextInteractor> context)
    {
        ContextsScenesConfiguration contextsScenesConfiguration = ServiceLocator.Get<ContextsScenesConfiguration>();
        GameConfiguration gameConfiguration = ServiceLocator.Get<GameConfiguration>();
        IRootNodeService rootNodeService = ServiceLocator.Get<IRootNodeService>();
        
        context.AddInstallerLoadable(new PackedSceneNodeInstallerLoadable(contextsScenesConfiguration.GameContextPrefab!, rootNodeService.Root));
        
        context.AddInstaller(new CallbackInstaller(
            b =>
            {
                b.InstallGameGeneralConfigurations(_contextConfiguration, gameConfiguration);
                b.InstallGameGeneralServices();
                b.InstallGameGeneral();
                
                b.InstallGameCameras();
                b.InstallGamePlayers();
                b.InstallGameMaps();
                b.InstallGameAreas();
                b.InstallGameConnections();
                b.InstallGameCollectables();
                b.InstallGameVisualEffects();
                b.InstallGameCrates();
                b.InstallGameTrampolines();
                b.InstallGameVelocityBoosters();
                b.InstallGamePlayerKillers();
                b.InstallGameFruits();
                b.InstallGameCinematics();
                b.InstallGameDialogues();
                b.InstallGameLetters();
                b.InstallGamePause();
                b.InstallGameOutro();
                
                b.InstallGameCheats();
            }));
    }
}