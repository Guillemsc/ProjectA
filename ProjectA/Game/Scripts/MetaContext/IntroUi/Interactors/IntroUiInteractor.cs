using System.Threading;
using System.Threading.Tasks;
using Game.MetaContext.IntroUi.UseCases;

namespace Game.MetaContext.IntroUi.Interactors;

public sealed class IntroUiInteractor : IIntroUiInteractor
{
    readonly SetVisibleUseCase _setVisibleUseCase;

    public IntroUiInteractor(
        SetVisibleUseCase setVisibleUseCase
        )
    {
        _setVisibleUseCase = setVisibleUseCase;
    }

    public Task SetVisible(bool visible, bool instantly, CancellationToken cancellationToken)
        => _setVisibleUseCase.Execute(visible, instantly, cancellationToken);
}