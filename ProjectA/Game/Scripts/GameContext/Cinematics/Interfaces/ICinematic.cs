using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Cinematics.Contexts;

namespace Game.GameContext.Cinematics.Interfaces;

public interface ICinematic
{
    Task PlayCinematic(
        CinematicsContext context, 
        CancellationToken skipToken,
        CancellationToken cancellationToken
        );
}