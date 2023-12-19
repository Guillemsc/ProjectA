using System.Threading;
using System.Threading.Tasks;

namespace Game.LoadingScreenContext.General.UseCases;

public sealed class LoadingScreenLoadUseCase
{
    readonly LinkToLoadingServiceUseCase _linkToLoadingServiceUseCase;

    public LoadingScreenLoadUseCase(
        LinkToLoadingServiceUseCase linkToLoadingServiceUseCase
        )
    {
        _linkToLoadingServiceUseCase = linkToLoadingServiceUseCase;
    }

    public Task Execute(CancellationToken cancellationToken)
    {
        _linkToLoadingServiceUseCase.Execute();
        return Task.CompletedTask;
    }
}