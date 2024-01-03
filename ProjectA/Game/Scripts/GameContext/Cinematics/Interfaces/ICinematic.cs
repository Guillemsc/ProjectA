using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Cinematics.Contexts;

namespace Game.GameContext.Cinematics.Interfaces;

public interface ICinematic
{
    Task Play(
        CinematicContext context, 
        CancellationToken skipToken,
        CancellationToken cancellationToken
        );
}