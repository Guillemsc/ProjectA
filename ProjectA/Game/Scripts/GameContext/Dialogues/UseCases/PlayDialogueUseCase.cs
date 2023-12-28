using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Dialogues.Configurations;
using Game.GameContext.DialogueUi.Interactors;
using GUtils.Locations.Enums;

namespace Game.GameContext.Dialogues.UseCases;

public sealed class PlayDialogueUseCase
{
    readonly IDialogueUiInteractor _dialogueUiInteractor;
    readonly PlayDialogueEntryUseCase _playDialogueEntryUseCase;
    readonly AwaitDialogueContinueInputUseCase _awaitDialogueContinueInputUseCase;

    public PlayDialogueUseCase(
        IDialogueUiInteractor dialogueUiInteractor,
        PlayDialogueEntryUseCase playDialogueEntryUseCase, 
        AwaitDialogueContinueInputUseCase awaitDialogueContinueInputUseCase
        )
    {
        _dialogueUiInteractor = dialogueUiInteractor;
        _playDialogueEntryUseCase = playDialogueEntryUseCase;
        _awaitDialogueContinueInputUseCase = awaitDialogueContinueInputUseCase;
    }

    public async Task Execute(DialogueConfiguration dialogueConfiguration, CancellationToken cancellationToken)
    {
        DialogueEntryConfiguration? previousDialogueEntry = null;
        
        foreach (DialogueEntryConfiguration dialogueEntry in dialogueConfiguration.Entries!)
        {
            bool sameAsBefore = previousDialogueEntry != null
                && previousDialogueEntry.PortraitLocation == dialogueEntry.PortraitLocation;
            
            if (!sameAsBefore)
            {
                await _dialogueUiInteractor.SetVisible(false, false, cancellationToken);
                _dialogueUiInteractor.SetupDialogue(dialogueEntry.PortraitLocation);
                await _dialogueUiInteractor.SetVisible(true, false, cancellationToken);
            }

            previousDialogueEntry = dialogueEntry;
            
            await _playDialogueEntryUseCase.Execute(dialogueEntry, cancellationToken);

            await _awaitDialogueContinueInputUseCase.Execute(cancellationToken);
        }
        
        await _dialogueUiInteractor.SetVisible(false, false, cancellationToken);
    }
}