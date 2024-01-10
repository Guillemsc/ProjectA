using System.Threading;
using System.Threading.Tasks;
using Godot;
using GUtilsGodot.Extensions;

namespace Game.GameContext.OutroUi.UseCases;

public sealed class SetVisibleUseCase
{
    readonly AnimationPlayer _animationPlayer;

    public SetVisibleUseCase(AnimationPlayer animationPlayer)
    {
        _animationPlayer = animationPlayer;
    }

    public Task Execute(bool visible, bool instantly, CancellationToken cancellationToken)
    {
        string animation = visible ? "Show" : "Hide";
        
        return _animationPlayer.PlayAndAwaitCompletition(animation, instantly, cancellationToken);
    }
}