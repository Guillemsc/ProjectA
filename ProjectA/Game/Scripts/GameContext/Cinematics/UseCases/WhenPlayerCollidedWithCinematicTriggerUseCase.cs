using Game.GameContext.Cinematics.Views;

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
        _playCinematicUseCase.Execute(cinematicTriggerView.CinematicView!);
    }
}