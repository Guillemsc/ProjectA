using System.Threading;
using System.Threading.Tasks;
using Godot;
using GUtilsGodot.Extensions;

namespace Game.GameContext.VisualEffects.Views;

public partial class AnimatedSprite2DOneShotVisualEffectView : OneShotVisualEffectView
{
    [Export] AnimatedSprite2D? AnimatedSprite2D;
    [Export] string? Animation;

    public sealed override Task Play(CancellationToken cancellationToken)
    {
        AnimatedSprite2D!.Play(Animation);
        
        return AnimatedSprite2D.AwaitCompletition(cancellationToken);
    }
}