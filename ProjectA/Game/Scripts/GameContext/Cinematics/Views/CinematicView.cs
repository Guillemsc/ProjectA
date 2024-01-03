using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Cinematics.Contexts;
using Game.GameContext.Cinematics.Interfaces;
using Godot;

namespace Game.GameContext.Cinematics.Views;

public partial class CinematicView : Node2D, ICinematic
{
    public virtual Task Play(
        CinematicContext context, 
        CancellationToken skipToken,
        CancellationToken cancellationToken
        ) => Task.CompletedTask;
}