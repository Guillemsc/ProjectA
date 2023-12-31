using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.PauseUi.UseCases;

namespace Game.GameContext.PauseUi.Interactors;

public sealed class PauseUiInteractor : IPauseUiInteractor
{
    readonly SetVisibleUseCase _setVisibleUseCase;

    public PauseUiInteractor(
        SetVisibleUseCase setVisibleUseCase
        )
    {
        _setVisibleUseCase = setVisibleUseCase;
    }

    public Task SetVisible(bool visible, bool instantly, CancellationToken cancellationToken)
        => _setVisibleUseCase.Execute(visible, instantly, cancellationToken);
}