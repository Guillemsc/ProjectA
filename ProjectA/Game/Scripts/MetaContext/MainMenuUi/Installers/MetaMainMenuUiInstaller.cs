using Game.GameContext.General.Configurations;
using Game.General.Buttons;
using Game.MetaContext.MainMenuUi.Interactors;
using Game.MetaContext.MainMenuUi.UseCases;
using Godot;
using GUtils.Di.Builder;
using GUtils.Di.Installers;
using GUtils.Loading.Services;
using GUtilsGodot.Extensions;
using GUtilsGodot.UiStack.Entries;
using GUtilsGodot.UiStack.Enums;
using GUtilsGodot.UiStack.Extensions;
using GUtilsGodot.UiStack.Services;
using GUtilsGodot.Visibility.Visibles;
using GameMenuButton = Game.General.Buttons.Nodes.GameMenuButton;

namespace Game.MetaContext.MainMenuUi.Installers;

public partial class MetaMainMenuUiInstaller : Control, IInstaller
{
    [Export] public AnimationPlayer? AnimationPlayer;
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
                    new AnimationPlayerVisible(AnimationPlayer!, "Show", "Hide"),
                    false,
                    new []
                    {
                        new UiStackEntryRefresh(RefreshType.BeforeShow, c.Resolve<SetInitialFocusUseCase>())
                    }
                    )
            );

        builder.Bind<WhenPlayButtonPressedUseCase>()
            .FromFunction(c => new WhenPlayButtonPressedUseCase(
                c.Resolve<ILoadingService>(),
                c.Resolve<IUiStackService>(),
                c.Resolve<GameConfiguration>(),
                c.Resolve<IMainMenuUiInteractor>()
            ))
            .LinkButtonPressed(PlayButton!);

        builder.Bind<SetInitialFocusUseCase>()
            .FromFunction(c => new SetInitialFocusUseCase(
                PlayButton!
            ));
    }
}