using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Cinematics.Contexts;
using Game.GameContext.Cinematics.Interfaces;
using Godot;

namespace Game.GameContext.Cinematics.Views;

public partial class CinematicView : Node2D, ICinematic
{
    public virtual Task PlayCinematic(CinematicsContext cinematicsContext, CancellationToken cancellationToken) => Task.CompletedTask;
}