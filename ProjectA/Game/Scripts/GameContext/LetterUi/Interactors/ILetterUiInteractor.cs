using GUtils.Visibility.Visibles;

namespace Game.GameContext.LetterUi.Interactors;

public interface ILetterUiInteractor : IVisible
{
    void SetText(string text);
}