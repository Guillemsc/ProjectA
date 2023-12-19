using System.Threading;
using System.Threading.Tasks;
using Game.LoadingScreenContext.MapTransitionUi.UseCases;

namespace Game.LoadingScreenContext.MapTransitionUi.Interactors;

public sealed class MapTransitionUiInteractor : IMapTransitionUiInteractor
{
    readonly SetVisibleUseCase _setVisibleUseCase;

    public MapTransitionUiInteractor(SetVisibleUseCase setVisibleUseCase)
    {
        _setVisibleUseCase = setVisibleUseCase;
    }

    public Task SetVisible(bool visible, bool instantly, CancellationToken cancellationToken)
        => _setVisibleUseCase.Execute(visible, instantly, cancellationToken);
}