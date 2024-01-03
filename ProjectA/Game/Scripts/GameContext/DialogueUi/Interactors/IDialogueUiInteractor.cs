using System.Threading;
using System.Threading.Tasks;
using Godot;
using GUtils.Locations.Enums;
using GUtils.Visibility.Visibles;

namespace Game.GameContext.DialogueUi.Interactors;

public interface IDialogueUiInteractor : IVisible
{
    void SetupDialogue(HorizontalLocation portraitLocation, Texture2D portraitTexture);
    Task ShowText(string text, CancellationToken cancellationToken);
    bool IsShowingText();
    void CompleteTextShowing();
}