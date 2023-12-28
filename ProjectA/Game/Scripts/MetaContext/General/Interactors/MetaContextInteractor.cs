using System.Threading;
using System.Threading.Tasks;
using Game.MetaContext.General.UseCases;

namespace Game.MetaContext.General.Interactors;

public sealed class MetaContextInteractor : IMetaContextInteractor
{
    readonly MetaStartUseCase _metaStartUseCase;

    public MetaContextInteractor(MetaStartUseCase metaStartUseCase)
    {
        _metaStartUseCase = metaStartUseCase;
    }

    public Task Load(CancellationToken cancellationToken)
        => Task.CompletedTask;

    public void Start()
        => _metaStartUseCase.Execute();

    public Task Dispose(CancellationToken cancellationToken)
        => Task.CompletedTask;
}