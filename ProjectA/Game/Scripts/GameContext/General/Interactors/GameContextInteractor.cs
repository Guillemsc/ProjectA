using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.General.UseCases;

namespace Game.GameContext.General.Interactors;

public sealed class GameContextInteractor : IGameContextInteractor
{
    readonly GameLoadUseCase _gameLoadUseCase;
    readonly GameStartUseCase _gameStartUseCase;

    public GameContextInteractor(
        GameLoadUseCase gameLoadUseCase, 
        GameStartUseCase gameStartUseCase
        )
    {
        _gameLoadUseCase = gameLoadUseCase;
        _gameStartUseCase = gameStartUseCase;
    }

    public Task Load(CancellationToken cancellationToken)
        => _gameLoadUseCase.Execute(cancellationToken);

    public void Start()
        => _gameStartUseCase.Execute();

    public Task Dispose(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}