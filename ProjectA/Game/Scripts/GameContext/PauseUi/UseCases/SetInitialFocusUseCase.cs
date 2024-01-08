using Game.General.Buttons.Nodes;

namespace Game.GameContext.PauseUi.UseCases;

public sealed class SetInitialFocusUseCase
{
    readonly GameMenuButton _button;

    public SetInitialFocusUseCase(GameMenuButton button)
    {
        _button = button;
    }

    public void Execute()
    {
        _button.GameMenuButtonGrabFocus();
    }
}