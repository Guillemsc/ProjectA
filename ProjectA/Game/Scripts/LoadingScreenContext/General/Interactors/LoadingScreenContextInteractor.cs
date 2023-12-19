using System.Threading;
using System.Threading.Tasks;
using Game.LoadingScreenContext.General.UseCases;

namespace Game.LoadingScreenContext.General.Interactors;

public sealed class LoadingScreenContextInteractor : ILoadingScreenContextInteractor
{
    readonly LoadingScreenLoadUseCase _loadingScreenLoadUseCase;

    public LoadingScreenContextInteractor(
        LoadingScreenLoadUseCase loadingScreenLoadUseCase
        )
    {
        _loadingScreenLoadUseCase = loadingScreenLoadUseCase;
    }

    public Task Load(CancellationToken cancellationToken)
        => _loadingScreenLoadUseCase.Execute(cancellationToken);

    public void Start()
    {
        
    }

    public Task Dispose(CancellationToken cancellationToken)
        => Task.CompletedTask;
}