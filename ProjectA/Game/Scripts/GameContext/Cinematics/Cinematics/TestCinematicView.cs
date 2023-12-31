using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Cinematics.Contexts;
using Game.GameContext.Cinematics.Views;
using Game.GameContext.Players.Enums;
using Game.GameContext.Players.Views;
using GTweens.Builders;
using GTweens.Easings;
using GTweens.Tweens;
using GTweensGodot.Extensions;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Cinematics.Cinematics;

public partial class TestCinematicView : CinematicView
{
    public override async Task Play(
        CinematicContext context, 
        CancellationToken skipToken,
        CancellationToken cancellationToken
        )
    {
        await context.Methods.AwaitUntilPlayerIsOnTheGroundUseCase.Execute(cancellationToken);
        
        PlayerView playerView = context.PlayerView;

        playerView.CanUpdateMovement = false;
        playerView.AnimationPlayer!.ProcessMode = ProcessModeEnum.Disabled;
        playerView.AnimatedSprite!.Play(PlayerAnimationState.Idle);

        float newPosition = playerView.GlobalPosition.X - 50;
        
        GTweenSequenceBuilder sequenceBuilder = GTweenSequenceBuilder.New();
        
        sequenceBuilder.AppendTime(1f)
            .AppendCallback(() => playerView.AnimatedSprite!.FlipH = true)
            .AppendTime(1f)
            .AppendCallback(() => playerView.AnimatedSprite!.Play(PlayerAnimationState.Run))
            .Append(playerView.TweenPositionX(newPosition, 1f).SetEasing(Easing.Linear))
            .AppendCallback(() => playerView.AnimatedSprite!.Play(PlayerAnimationState.Idle))
            .AppendTime(1f);

        GTween tween = sequenceBuilder.Build();
        
        await tween.PlayAsync(cancellationToken);
        
        playerView.CanUpdateMovement = true;
        playerView.AnimationPlayer!.ProcessMode = ProcessModeEnum.Inherit;
    }
}