﻿using System;
using Game.Contexts.Configuration;
using Game.GameContext.General.Installers;
using Game.GameContext.General.Interactors;
using GUtils.ApplicationContexts.Contexts;
using GUtils.Di.Contexts;
using GUtils.Di.Installers;
using GUtils.Services.Locators;
using GUtilsGodot.Di.Loadables;
using GUtilsGodot.Roots.Services;

namespace Game.GameContext.General.Contexts;

public sealed class GameApplicationContext : DiApplicationContext<IGameContextInteractor>
{
    protected override void Install(IDiContext<IGameContextInteractor> context)
    {
        ContextsScenesConfiguration contextsScenesConfiguration = ServiceLocator.Get<ContextsScenesConfiguration>();
        IRootNodeService rootNodeService = ServiceLocator.Get<IRootNodeService>();
        
        context.AddInstallerLoadable(new PackedSceneNodeInstallerLoadable(contextsScenesConfiguration.GameContextPrefab, rootNodeService.Root));
        
        context.AddInstaller(new CallbackInstaller(
            b => b.InstallGameInteractors()
        ));
    }
}