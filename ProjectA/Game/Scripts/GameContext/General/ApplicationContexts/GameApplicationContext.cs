﻿using Game.Contexts.Configuration;
using Game.GameContext.Cheats.Installers;
using Game.GameContext.Collectables.Installers;
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
    protected override void Install(IDiContext<IGameContextInteractor> context)
    {
        ContextsScenesConfiguration contextsScenesConfiguration = ServiceLocator.Get<ContextsScenesConfiguration>();
        GameConfiguration gameConfiguration = ServiceLocator.Get<GameConfiguration>();
        IRootNodeService rootNodeService = ServiceLocator.Get<IRootNodeService>();
        
        context.AddInstallerLoadable(new PackedSceneNodeInstallerLoadable(contextsScenesConfiguration.GameContextPrefab!, rootNodeService.Root));
        
        context.AddInstaller(new CallbackInstaller(
            b =>
            {
                b.InstallGameGeneralConfigurations(gameConfiguration);
                b.InstallGameGeneralServices();
                b.InstallGameGeneral();
                
                b.InstallGamePlayers();
                b.InstallGameMaps();
                b.InstallGameCollectables();
                
                b.InstallGameCheats();
            }));
    }
}