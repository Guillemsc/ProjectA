using System.Threading;
using System.Threading.Tasks;
using Game.MetaContext.General.UseCases;

namespace Game.MetaContext.General.Interactors;

public sealed class MetaContextInteractor : IMetaContextInteractor
{
    readonly MetaLoadUseCase _metaLoadUseCase;
    readonly MetaStartUseCase _metaStartUseCase;

    public MetaContextInteractor(
        MetaLoadUseCase metaLoadUseCase, 
        MetaStartUseCase metaStartUseCase
        )
    {
        _metaStartUseCase = metaStartUseCase;
        _metaLoadUseCase = metaLoadUseCase;
    }

    public Task Load(CancellationToken cancellationToken)
        => _metaLoadUseCase.Execute(cancellationToken);

    public void Start()
        => _metaStartUseCase.Execute();

    public Task Dispose(CancellationToken cancellationToken)
        => Task.CompletedTask;
}