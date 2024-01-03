using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Dialogues.Configurations;
using Game.GameContext.Dialogues.Datas;
using Game.GameContext.DialogueUi.Interactors;
using GUtils.Extensions;
using GUtils.Optionals;

namespace Game.GameContext.Dialogues.UseCases;

public sealed class PlayDialogueUseCase
{
    readonly IDialogueUiInteractor _dialogueUiInteractor;
    readonly CurrentDialogueData _currentDialogueData;
    readonly PlayDialogueEntryUseCase _playDialogueEntryUseCase;
    readonly AwaitDialogueContinueInputUseCase _awaitDialogueContinueInputUseCase;
    readonly GetDialogueSpeakerConfigurationUseCase _getDialogueSpeakerConfigurationUseCase;

    public PlayDialogueUseCase(
        IDialogueUiInteractor dialogueUiInteractor,
        CurrentDialogueData currentDialogueData,
        PlayDialogueEntryUseCase playDialogueEntryUseCase, 
        AwaitDialogueContinueInputUseCase awaitDialogueContinueInputUseCase, 
        GetDialogueSpeakerConfigurationUseCase getDialogueSpeakerConfigurationUseCase
        )
    {
        _dialogueUiInteractor = dialogueUiInteractor;
        _currentDialogueData = currentDialogueData;
        _playDialogueEntryUseCase = playDialogueEntryUseCase;
        _awaitDialogueContinueInputUseCase = awaitDialogueContinueInputUseCase;
        _getDialogueSpeakerConfigurationUseCase = getDialogueSpeakerConfigurationUseCase;
    }

    public async Task Execute(
        DialogueConfiguration dialogueConfiguration, 
        CancellationToken skipToken,
        CancellationToken cancellationToken
        )
    {
        if (_currentDialogueData.IsPlayingDialogue)
        {
            return;
        }

        _currentDialogueData.IsPlayingDialogue = true;
        
        CancellationTokenSource allCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            skipToken,
            cancellationToken
        );
        CancellationToken allCancellationToken = allCancellationTokenSource.Token;
        
        DialogueEntryConfiguration? previousDialogueEntry = null;
        
        foreach (DialogueEntryConfiguration dialogueEntry in dialogueConfiguration.Entries!)
        {
            bool sameAsBefore = previousDialogueEntry != null
                && previousDialogueEntry.PortraitLocation == dialogueEntry.PortraitLocation;
            
            if (!sameAsBefore)
            {
                await _dialogueUiInteractor.SetVisible(false, false, allCancellationToken);
                
                if (allCancellationToken.IsCancellationRequested) break;

                Optional<DialogueSpeakerConfiguration> optionalSpeakerConfiguration =
                    _getDialogueSpeakerConfigurationUseCase.Execute(
                        dialogueEntry.Speaker
                    );

                bool hasSpeakerConfiguration =
                    optionalSpeakerConfiguration.TryGet(out DialogueSpeakerConfiguration speakerConfiguration);

                if (!hasSpeakerConfiguration)
                {
                    continue;
                }
                
                _dialogueUiInteractor.SetupDialogue(dialogueEntry.PortraitLocation, speakerConfiguration.PortraitTexture!);
                await _dialogueUiInteractor.SetVisible(true, false, allCancellationToken);
                
                if (allCancellationToken.IsCancellationRequested) break;
            }

            previousDialogueEntry = dialogueEntry;
            
            _playDialogueEntryUseCase.Execute(dialogueEntry, allCancellationToken).RunAsync();
            
            await _awaitDialogueContinueInputUseCase.Execute(allCancellationToken);

            bool isShowingText = _dialogueUiInteractor.IsShowingText();

            if (isShowingText)
            {
                _dialogueUiInteractor.CompleteTextShowing();
                
                await _awaitDialogueContinueInputUseCase.Execute(allCancellationToken);
            }

            if (allCancellationToken.IsCancellationRequested) break;
        }
        
        await _dialogueUiInteractor.SetVisible(false, false, cancellationToken);
        
        allCancellationTokenSource.Dispose();
        
        _currentDialogueData.IsPlayingDialogue = false;
    }
}