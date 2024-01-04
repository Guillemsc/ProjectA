using Game.GameContext.Cinematics.UseCases;
using Game.GameContext.Pause.UseCases;
using Godot;

namespace Game.GameContext.PauseUi.UseCases;

public sealed class WhenSkipCinematicButtonPressedUseCase
{
    readonly ToggleGamePauseUiUseCase _toggleGamePauseUiUseCase;
    readonly SkipCurrentCinematicUseCase _skipCurrentCinematicUseCase;

    public WhenSkipCinematicButtonPressedUseCase(
        ToggleGamePauseUiUseCase toggleGamePauseUiUseCase,
        SkipCurrentCinematicUseCase skipCurrentCinematicUseCase
        )
    {
        _skipCurrentCinematicUseCase = skipCurrentCinematicUseCase;
        _toggleGamePauseUiUseCase = toggleGamePauseUiUseCase;
    }

    public void Execute()
    {
        _toggleGamePauseUiUseCase.Execute();
        _skipCurrentCinematicUseCase.Execute();
    }
}