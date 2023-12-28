using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.DialogueUi.UseCases;
using GUtils.Locations.Enums;

namespace Game.GameContext.DialogueUi.Interactors;

public sealed class DialogueUiInteractor : IDialogueUiInteractor
{
    readonly SetVisibleUseCase _setVisibleUseCase;
    readonly ShowTextUseCase _showTextUseCase;
    readonly SetupDialogueUseCase _setupDialogueUseCase;

    public DialogueUiInteractor(
        SetVisibleUseCase setVisibleUseCase, 
        ShowTextUseCase showTextUseCase, 
        SetupDialogueUseCase setupDialogueUseCase
        )
    {
        _setVisibleUseCase = setVisibleUseCase;
        _showTextUseCase = showTextUseCase;
        _setupDialogueUseCase = setupDialogueUseCase;
    }

    public Task SetVisible(bool visible, bool instantly, CancellationToken cancellationToken)
        => _setVisibleUseCase.Execute(visible, instantly, cancellationToken);

    public Task ShowText(string text, CancellationToken cancellationToken)
        => _showTextUseCase.Execute(text, cancellationToken);
    
    public void SetupDialogue(HorizontalLocation portraitLocation)
        => _setupDialogueUseCase.Execute(portraitLocation);
}