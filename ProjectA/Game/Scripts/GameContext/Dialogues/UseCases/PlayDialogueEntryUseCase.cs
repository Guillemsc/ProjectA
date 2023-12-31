using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Dialogues.Configurations;
using Game.GameContext.DialogueUi.Interactors;

namespace Game.GameContext.Dialogues.UseCases;

public sealed class PlayDialogueEntryUseCase
{
    readonly IDialogueUiInteractor _dialogueUiInteractor;

    public PlayDialogueEntryUseCase(IDialogueUiInteractor dialogueUiInteractor)
    {
        _dialogueUiInteractor = dialogueUiInteractor;
    }

    public Task Execute(DialogueEntryConfiguration dialogueEntry, CancellationToken cancellationToken)
    {
        return _dialogueUiInteractor.ShowText(
            dialogueEntry.Text!,
            cancellationToken
        );
    }
}