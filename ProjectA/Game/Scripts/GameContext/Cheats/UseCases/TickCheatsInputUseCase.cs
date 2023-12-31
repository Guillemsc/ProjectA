using Game.GameContext.Cinematics.UseCases;
using Game.GameContext.DialogueUi.Interactors;
using Godot;

namespace Game.GameContext.Cheats.UseCases;

public sealed class TickCheatsInputUseCase
{
    readonly RestartCheatUseCase _restartCheatUseCase;
    readonly SkipCurrentCinematicUseCase _skipCurrentCinematicUseCase;

    public TickCheatsInputUseCase(
        RestartCheatUseCase restartCheatUseCase, 
        SkipCurrentCinematicUseCase skipCurrentCinematicUseCase)
    {
        _restartCheatUseCase = restartCheatUseCase;
        _skipCurrentCinematicUseCase = skipCurrentCinematicUseCase;
    }

    public void Execute()
    {
        if (Input.IsActionJustPressed("RestartCheat"))
        {
            _restartCheatUseCase.Execute();
        }
    }
}