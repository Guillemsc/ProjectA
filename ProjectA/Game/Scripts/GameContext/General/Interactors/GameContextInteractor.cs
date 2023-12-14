using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.General.UseCases;

namespace Game.GameContext.General.Interactors;

public sealed class GameContextInteractor : IGameContextInteractor
{
    readonly GameLoadUseCase _gameLoadUseCase;

    public GameContextInteractor(GameLoadUseCase gameLoadUseCase)
    {
        _gameLoadUseCase = gameLoadUseCase;
    }

    public Task Load(CancellationToken cancellationToken)
        => _gameLoadUseCase.Execute(cancellationToken);

    public void Start()
    {
        
    }

    public Task Dispose(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}