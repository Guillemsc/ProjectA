using System.Threading;
using System.Threading.Tasks;
using Godot;

namespace Game.GameContext.VisualEffects.Views;

public partial class OneShotVisualEffectView : Node2D
{
    public virtual Task Play(CancellationToken cancellationToken) => Task.CompletedTask;
}