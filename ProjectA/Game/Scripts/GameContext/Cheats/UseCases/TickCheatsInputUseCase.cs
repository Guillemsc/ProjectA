using Godot;

namespace Game.GameContext.Cheats.UseCases;

public sealed class TickCheatsInputUseCase
{
    readonly RestartCheatUseCase _restartCheatUseCase;

    public TickCheatsInputUseCase(RestartCheatUseCase restartCheatUseCase)
    {
        _restartCheatUseCase = restartCheatUseCase;
    }

    public void Execute()
    {
        if (Input.IsActionJustPressed("RestartCheat"))
        {
            _restartCheatUseCase.Execute();
        }
    }
}