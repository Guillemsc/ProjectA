using Game.GameContext.DialogueUi.Data;

namespace Game.GameContext.DialogueUi.UseCases;

public sealed class CompleteTextShowingUseCase
{
    readonly DialogueUiTweensData _dialogueUiTweensData;

    public CompleteTextShowingUseCase(
        DialogueUiTweensData dialogueUiTweensData
        )
    {
        _dialogueUiTweensData = dialogueUiTweensData;
    }

    public void Execute()
    {
        _dialogueUiTweensData.CurrentDialogueTween?.Complete();
    }
}