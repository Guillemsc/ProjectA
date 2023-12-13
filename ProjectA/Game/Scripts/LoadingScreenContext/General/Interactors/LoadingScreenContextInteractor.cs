using System.Threading;
using System.Threading.Tasks;

namespace Game.LoadingScreenContext.General.Interactors;

public sealed class LoadingScreenContextInteractor : ILoadingScreenContextInteractor
{
    public Task Load(CancellationToken cancellationToken)
        => Task.CompletedTask;

    public void Start()
    {
        
    }

    public Task Dispose(CancellationToken cancellationToken)
        => Task.CompletedTask;
}