using Game.GameContext.General.Configurations;
using Game.MetaContext.MainMenuUi.Interactors;
using Game.MetaContext.MainMenuUi.UseCases;
using Godot;
using GUtils.Di.Builder;
using GUtils.Di.Installers;
using GUtils.Loading.Services;
using GUtilsGodot.Extensions;
using GUtilsGodot.UiFrame.Services;
using GUtilsGodot.UiStack.Extensions;
using GUtilsGodot.UiStack.Services;
using GUtilsGodot.Visibility.Visibles;

namespace Game.MetaContext.MainMenuUi.Installers;

public partial class MetaMainMenuUiInstaller : Control, IInstaller
{
    [Export] public AnimationPlayer? AnimationPlayer;
    [Export] public Button? PlayButton;
    
    public void Install(IDiContainerBuilder builder)
    {
        builder.Bind<IMainMenuUiInteractor>()
            .FromFunction(c => new MainMenuUiInteractor())
            .LinkUiToUiStackService(
                new AnimationPlayerVisible(AnimationPlayer!, "Show", "Hide"),
                this
            );

        builder.Bind<WhenPlayButtonPressedUseCase>()
            .FromFunction(c => new WhenPlayButtonPressedUseCase(
                c.Resolve<ILoadingService>(),
                c.Resolve<IUiStackService>(),
                c.Resolve<GameConfiguration>(),
                c.Resolve<IMainMenuUiInteractor>()
            ))
            .LinkButtonPressed(PlayButton!);
    }
}