using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.General.ApplicationContexts;
using Game.GameContext.General.Configurations;
using Godot;
using GUtils.Directions;
using GUtils.Loading.Extensions;
using GUtils.Loading.Services;
using GUtils.Services.Locators;
using GUtilsGodot.Bootstraps;
using ContextsScenesConfiguration = Game.General.Contexts.Configuration.ContextsScenesConfiguration;

namespace Game.Bootstraps;

public partial class TestMapBootstrap : Bootstrap
{
    [Export] public ContextsScenesConfiguration? ContextsScenesConfiguration;
    [Export] public GameConfiguration? GameConfiguration;
    [Export(PropertyHint.File, "*.tscn,*.scn")] public string? TestMap;
    
    protected override async Task Run(CancellationToken cancellationToken)
    {
        await CoreBootstrapSetup.Execute(
            this,
            ContextsScenesConfiguration!,
            GameConfiguration!,
            cancellationToken
        );

        ILoadingService loadingService = ServiceLocator.Get<ILoadingService>();

        GameApplicationContextConfiguration contextConfiguration = new(
            TestMap!,
            string.Empty,
            true,
            HorizontalDirection.Right
        );
        
        await loadingService.New()
            .RunBeforeLoadActionsInstantly()
            .EnqueueLoadAndStartApplicationContext(new GameApplicationContext(contextConfiguration))
            .Execute(cancellationToken);
    }
}