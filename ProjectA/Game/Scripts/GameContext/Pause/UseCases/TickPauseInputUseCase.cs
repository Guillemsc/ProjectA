using Godot;
using GUtils.Executables;

namespace Game.GameContext.Pause.UseCases;

public sealed class TickPauseInputUseCase : IExecutable
{
    readonly ToggleGamePauseUiUseCase _toggleGamePauseUiUseCase;

    public TickPauseInputUseCase(
        ToggleGamePauseUiUseCase toggleGamePauseUiUseCase
        )
    {
        _toggleGamePauseUiUseCase = toggleGamePauseUiUseCase;
    }

    public void Execute()
    {
        bool pauseInput = Input.IsActionJustPressed("ui_cancel");

        if (!pauseInput)
        {
            return;
        }
        
        _toggleGamePauseUiUseCase.Execute();
    }
}