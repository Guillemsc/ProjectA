using System.Threading;
using System.Threading.Tasks;
using GUtils.Locations.Enums;
using GUtils.Visibility.Visibles;

namespace Game.GameContext.DialogueUi.Interactors;

public interface IDialogueUiInteractor : IVisible
{
    void SetupDialogue(HorizontalLocation portraitLocation);
    Task ShowText(string text, CancellationToken cancellationToken);
}