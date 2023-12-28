using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.LetterUi.UseCases;

namespace Game.GameContext.LetterUi.Interactors;

public sealed class LetterUiInteractor : ILetterUiInteractor
{
    readonly SetVisibleUseCase _setVisibleUseCase;
    readonly SetTextUseCase _setTextUseCase;

    public LetterUiInteractor(
        SetVisibleUseCase setVisibleUseCase, 
        SetTextUseCase setTextUseCase
        )
    {
        _setVisibleUseCase = setVisibleUseCase;
        _setTextUseCase = setTextUseCase;
    }

    public Task SetVisible(bool visible, bool instantly, CancellationToken cancellationToken)
        => _setVisibleUseCase.Execute(visible, instantly, cancellationToken);

    public void SetText(string text)
        => _setTextUseCase.Execute(text);
}