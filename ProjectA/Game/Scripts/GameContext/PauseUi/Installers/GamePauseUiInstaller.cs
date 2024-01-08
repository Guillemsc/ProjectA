using System.Threading;
using Game.GameContext.Cinematics.UseCases;
using Game.GameContext.Pause.UseCases;
using Game.GameContext.PauseUi.Interactors;
using Game.GameContext.PauseUi.UseCases;
using Game.General.Buttons.Nodes;
using Godot;
using GUtils.Di.Builder;
using GUtils.Extensions;
using GUtilsGodot.Di.Installers;
using GUtilsGodot.Extensions;

namespace Game.GameContext.PauseUi.Installers;

public partial class GamePauseUiInstaller : ControlInstaller
{
    [Export] public AnimationPlayer? AnimationPlayer;
    [Export] public GameMenuButton? ResumeButton;
    [Export] public GameMenuButton? SkipCinematicButton;

    public override void Install(IDiContainerBuilder builder)
    {
        builder.Bind<IPauseUiInteractor>()
            .FromFunction(c => new PauseUiInteractor(
                c.Resolve<SetVisibleUseCase>()
            ));
        
        builder.Bind<SetVisibleUseCase>()
            .FromFunction(c => new SetVisibleUseCase(
                AnimationPlayer!,
                c.Resolve<SetInitialFocusUseCase>()
            ))
            .WhenInit(o => o.Execute(false, true, CancellationToken.None).RunAsync())
            .NonLazy();;

        builder.Bind<SetInitialFocusUseCase>()
            .FromFunction(c => new SetInitialFocusUseCase(
                ResumeButton!
            ));
        
        builder.Bind<WhenResumeButtonPressedUseCase>()
            .FromFunction(c => new WhenResumeButtonPressedUseCase(
            ))
            .LinkButtonPressed(ResumeButton!, o => o.Execute);
        
        builder.Bind<WhenSkipCinematicButtonPressedUseCase>()
            .FromFunction(c => new WhenSkipCinematicButtonPressedUseCase(
                c.Resolve<ToggleGamePauseUiUseCase>(),
                c.Resolve<SkipCurrentCinematicUseCase>()
            ))
            .LinkButtonPressed(SkipCinematicButton!, o => o.Execute);
    }
}