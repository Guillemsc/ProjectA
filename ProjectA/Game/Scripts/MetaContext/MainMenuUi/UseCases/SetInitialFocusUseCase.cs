using GUtils.Executables;
using GameMenuButton = Game.General.Buttons.Nodes.GameMenuButton;

namespace Game.MetaContext.MainMenuUi.UseCases;

public sealed class SetInitialFocusUseCase : IExecutable
{
    readonly GameMenuButton _playButton;

    public SetInitialFocusUseCase(GameMenuButton playButton)
    {
        _playButton = playButton;
    }

    public void Execute()
    {
        _playButton.GameMenuButtonGrabFocus();
    }
}