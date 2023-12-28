using Game.GameContext.DialogueUi.Interactors;
using Godot;

namespace Game.GameContext.Cheats.UseCases;

public sealed class TickCheatsInputUseCase
{
    readonly RestartCheatUseCase _restartCheatUseCase;
    readonly IDialogueUiInteractor _dialogueUiInteractor;

    public TickCheatsInputUseCase(RestartCheatUseCase restartCheatUseCase, IDialogueUiInteractor dialogueUiInteractor)
    {
        _restartCheatUseCase = restartCheatUseCase;
        _dialogueUiInteractor = dialogueUiInteractor;
    }

    public void Execute()
    {
        if (Input.IsActionJustPressed("RestartCheat"))
        {
            _restartCheatUseCase.Execute();
        }
    }
}