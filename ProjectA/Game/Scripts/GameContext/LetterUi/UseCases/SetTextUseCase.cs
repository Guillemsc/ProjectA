using Godot;

namespace Game.GameContext.LetterUi.UseCases;

public sealed class SetTextUseCase
{
    readonly Label _textLabel;

    public SetTextUseCase(
        Label textLabel
        )
    {
        _textLabel = textLabel;
    }

    public void Execute(string text)
    {
        _textLabel.Text = text;
    }
}