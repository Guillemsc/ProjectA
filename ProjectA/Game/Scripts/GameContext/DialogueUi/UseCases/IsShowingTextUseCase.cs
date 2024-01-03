using Game.GameContext.DialogueUi.Data;

namespace Game.GameContext.DialogueUi.UseCases;

public sealed class IsShowingTextUseCase
{
    readonly DialoguePlayingData _dialoguePlayingData;

    public IsShowingTextUseCase(DialoguePlayingData dialoguePlayingData)
    {
        _dialoguePlayingData = dialoguePlayingData;
    }

    public bool Execute()
    {
        return _dialoguePlayingData.IsShowingText;
    }
}