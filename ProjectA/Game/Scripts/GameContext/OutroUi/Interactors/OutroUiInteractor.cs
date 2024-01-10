using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.OutroUi.UseCases;

namespace Game.GameContext.OutroUi.Interactors;

public sealed class OutroUiInteractor : IOutroUiInteractor
{
    readonly SetVisibleUseCase _setVisibleUseCase;

    public OutroUiInteractor(
        SetVisibleUseCase setVisibleUseCase
        )
    {
        _setVisibleUseCase = setVisibleUseCase;
    }

    public Task SetVisible(bool visible, bool instantly, CancellationToken cancellationToken)
        => _setVisibleUseCase.Execute(visible, instantly, cancellationToken);
}