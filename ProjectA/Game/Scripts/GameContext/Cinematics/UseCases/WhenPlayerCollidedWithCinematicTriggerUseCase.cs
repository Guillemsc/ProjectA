using Game.GameContext.Cinematics.Views;
using Godot;

namespace Game.GameContext.Cinematics.UseCases;

public sealed class WhenPlayerCollidedWithCinematicTriggerUseCase
{
    readonly PlayCinematicUseCase _playCinematicUseCase;

    public WhenPlayerCollidedWithCinematicTriggerUseCase(
        PlayCinematicUseCase playCinematicUseCase
        )
    {
        _playCinematicUseCase = playCinematicUseCase;
    }

    public void Execute(CinematicTriggerView cinematicTriggerView)
    {
        cinematicTriggerView.ProcessMode = Node.ProcessModeEnum.Disabled;
        
        _playCinematicUseCase.Execute(cinematicTriggerView.CinematicView!);
    }
}