using System.Threading;
using System.Threading.Tasks;

namespace Game.LoadingScreenContext.LoadingScreenUi.Interactors;

public sealed class LoadingScreenUiInteractor : ILoadingScreenUiInteractor
{
    public Task SetVisible(bool visible, bool instantly, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}