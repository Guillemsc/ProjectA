using Game.GameContext.General.Configurations;
using Game.MetaContext.MainMenuUi.Data;
using Game.MetaContext.MainMenuUi.Interactors;
using Game.MetaContext.MainMenuUi.UseCases;
using Godot;
using GUtils.Di.Builder;
using GUtils.Di.Installers;
using GUtils.Loading.Services;
using GUtils.Tasks.Runners;
using GUtils.Tick.Extensions;
using GUtilsGodot.Extensions;
using GUtilsGodot.Quitting.Services;
using GUtilsGodot.UiStack.Entries;
using GUtilsGodot.UiStack.Enums;
using GUtilsGodot.UiStack.Extensions;
using GUtilsGodot.UiStack.Services;
using GUtilsGodot.Visibility.Visibles;
using GameMenuButton = Game.General.Buttons.Nodes.GameMenuButton;

namespace Game.MetaContext.MainMenuUi.Installers;

public partial class MetaMainMenuUiInstaller : Control, IInstaller
{
    [Export] public AnimationPlayer? VisibilityAnimationPlayer;
    [Export] public AnimationPlayer? FoldingAnimationPlayer;
    [Export] public GameMenuButton? PlayButton;
    [Export] public GameMenuButton? OptionsButton;
    [Export] public GameMenuButton? CreditsButton;
    [Export] public GameMenuButton? ExitButton;
    
    public void Install(IDiContainerBuilder builder)
    {
        builder.Bind<IMainMenuUiInteractor>()
            .FromFunction(c => new MainMenuUiInteractor())
            .LinkToUiStackService(
                (c, o) => new UiStackEntry(
                    o,
                    this,
                    new AnimationPlayerVisible(VisibilityAnimationPlayer!, "Show", "Hide"),
                    false,
                    new []
                    {
                        new UiStackEntryRefresh(RefreshType.BeforeShow, c.Resolve<FoldInstantlyUseCase>()),
                        new UiStackEntryRefresh(RefreshType.AfterShow, c.Resolve<EnableCanUnfoldUseCase>())
                    }
                    )
            );

        builder.Bind<FoldedData>().FromNew();

        builder.Bind<EnableCanUnfoldUseCase>()
            .FromFunction(c => new EnableCanUnfoldUseCase(
                c.Resolve<FoldedData>()
            ));
        
        builder.Bind<UnfoldUseCase>()
            .FromFunction(c => new UnfoldUseCase(
                FoldingAnimationPlayer!,
                c.Resolve<FoldedData>(),
                c.Resolve<IAsyncTaskRunner>(),
                c.Resolve<SetInitialFocusUseCase>()
            ));

        builder.Bind<FoldInstantlyUseCase>()
            .FromFunction(c => new FoldInstantlyUseCase(
                FoldingAnimationPlayer!,
                c.Resolve<FoldedData>()
            ));

        builder.Bind<WhenPlayButtonPressedUseCase>()
            .FromFunction(c => new WhenPlayButtonPressedUseCase(
                c.Resolve<ILoadingService>(),
                c.Resolve<IUiStackService>(),
                c.Resolve<GameConfiguration>(),
                c.Resolve<IMainMenuUiInteractor>()
            ))
            .LinkButtonPressed(PlayButton!);

        builder.Bind<WhenQuitButtonPressedUseCase>()
            .FromFunction(c => new WhenQuitButtonPressedUseCase(
                c.Resolve<IQuitService>()
            ))
            .LinkButtonPressed(ExitButton!);

        builder.Bind<TickForUnfoldInputUseCase>()
            .FromFunction(c => new TickForUnfoldInputUseCase(
                c.Resolve<FoldedData>(),
                c.Resolve<UnfoldUseCase>()
            ))
            .LinkExecutableToTickablesService();

        builder.Bind<SetInitialFocusUseCase>()
            .FromFunction(c => new SetInitialFocusUseCase(
                PlayButton!
            ));
    }
}