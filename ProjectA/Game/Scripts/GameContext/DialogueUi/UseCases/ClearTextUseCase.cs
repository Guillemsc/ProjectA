using Godot;

namespace Game.GameContext.DialogueUi.UseCases;

public sealed class ClearTextUseCase
{
    readonly RichTextLabel _dialogueLabel;
    readonly Control _dialogueShownIndicatorControl;

    public ClearTextUseCase(
        RichTextLabel dialogueLabel, 
        Control dialogueShownIndicatorControl
        )
    {
        _dialogueLabel = dialogueLabel;
        _dialogueShownIndicatorControl = dialogueShownIndicatorControl;
    }
    
    public void Execute()
    {
        _dialogueLabel.Text = string.Empty;
        _dialogueShownIndicatorControl.Visible = false;
    }
}