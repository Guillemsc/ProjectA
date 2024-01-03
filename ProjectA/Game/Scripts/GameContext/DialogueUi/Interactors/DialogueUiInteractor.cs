using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.DialogueUi.UseCases;
using Godot;
using GUtils.Locations.Enums;

namespace Game.GameContext.DialogueUi.Interactors;

public sealed class DialogueUiInteractor : IDialogueUiInteractor
{
    readonly SetVisibleUseCase _setVisibleUseCase;
    readonly ShowTextUseCase _showTextUseCase;
    readonly SetupDialogueUseCase _setupDialogueUseCase;
    readonly IsShowingTextUseCase _isShowingTextUseCase;
    readonly CompleteTextShowingUseCase _completeTextShowingUseCase;

    public DialogueUiInteractor(
        SetVisibleUseCase setVisibleUseCase, 
        ShowTextUseCase showTextUseCase, 
        SetupDialogueUseCase setupDialogueUseCase,
        IsShowingTextUseCase isShowingTextUseCase,
        CompleteTextShowingUseCase completeTextShowingUseCase
    )
    {
        _setVisibleUseCase = setVisibleUseCase;
        _showTextUseCase = showTextUseCase;
        _setupDialogueUseCase = setupDialogueUseCase;
        _isShowingTextUseCase = isShowingTextUseCase;
        _completeTextShowingUseCase = completeTextShowingUseCase;
    }

    public Task SetVisible(bool visible, bool instantly, CancellationToken cancellationToken)
        => _setVisibleUseCase.Execute(visible, instantly, cancellationToken);

    public void SetupDialogue(HorizontalLocation portraitLocation, Texture2D portraitTexture)
        => _setupDialogueUseCase.Execute(portraitLocation, portraitTexture);
    
    public Task ShowText(string text, CancellationToken cancellationToken)
        => _showTextUseCase.Execute(text, cancellationToken);

    
    public bool IsShowingText()
        => _isShowingTextUseCase.Execute();

    public void CompleteTextShowing()
        => _completeTextShowingUseCase.Execute();
}