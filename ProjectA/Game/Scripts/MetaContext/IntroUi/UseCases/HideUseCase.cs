using System.Threading;
using System.Threading.Tasks;
using Godot;
using GUtilsGodot.Extensions;

namespace Game.MetaContext.IntroUi.UseCases;

public sealed class HideUseCase
{
    readonly AnimationPlayer _animationPlayer;

    public HideUseCase(AnimationPlayer animationPlayer)
    {
        _animationPlayer = animationPlayer;
    }

    public Task Execute(bool instantly, CancellationToken cancellationToken)
    {
        return _animationPlayer.PlayAndAwaitCompletition(
            "Hide",
            instantly,
            cancellationToken
        );
    }
}