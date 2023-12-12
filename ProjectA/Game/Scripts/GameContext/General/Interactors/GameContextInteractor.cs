using System.Threading;
using System.Threading.Tasks;

namespace Game.GameContext.General.Interactors;

public sealed class GameContextInteractor : IGameContextInteractor
{
    public Task Load(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void Start()
    {
        
    }

    public Task Dispose(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}