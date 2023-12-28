using Godot;
using GUtils.Locations.Enums;

namespace Game.GameContext.DialogueUi.UseCases;

public sealed class SetupDialogueUseCase
{
    readonly Control _dialoguePositionControl;
    readonly Control _leftDialoguePosition;
    readonly Control _rightDialoguePosition;
    readonly TextureRect _leftPortraitImage;
    readonly TextureRect _rightPortraitImage;

    public SetupDialogueUseCase(
        Control dialoguePositionControl, 
        Control leftDialoguePosition, 
        Control rightDialoguePosition, 
        TextureRect leftPortraitImage, 
        TextureRect rightPortraitImage
        )
    {
        _dialoguePositionControl = dialoguePositionControl;
        _leftDialoguePosition = leftDialoguePosition;
        _rightDialoguePosition = rightDialoguePosition;
        _leftPortraitImage = leftPortraitImage;
        _rightPortraitImage = rightPortraitImage;
    }

    public void Execute(HorizontalLocation portraitLocation)
    {
        Vector2 position = portraitLocation == HorizontalLocation.Left
            ? _leftDialoguePosition.GlobalPosition
            : _rightDialoguePosition.GlobalPosition;

        _dialoguePositionControl.GlobalPosition = position;

        TextureRect portraitToShow = portraitLocation == HorizontalLocation.Left ? _leftPortraitImage : _rightPortraitImage;
        TextureRect portraitToHide = portraitLocation == HorizontalLocation.Left ? _rightPortraitImage : _leftPortraitImage;

        portraitToShow.Visible = true;
        portraitToHide.Visible = false;
    }
}