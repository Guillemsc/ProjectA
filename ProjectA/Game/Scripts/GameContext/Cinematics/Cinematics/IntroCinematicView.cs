using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Cinematics.Contexts;
using Game.GameContext.Cinematics.Views;
using Game.GameContext.Players.Enums;
using Game.GameContext.Players.Views;
using Godot;
using GTweens.Builders;
using GTweens.Easings;
using GTweens.Tweens;
using GTweensGodot.Extensions;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Cinematics.Cinematics;

public partial class IntroCinematicView : CinematicView
{
    [Export] public Node2D? PositionBeforeJump;
    [Export] public float WalkTowardsJumpDuration = 1f;
    
    [Export] public Node2D? JumpHeightPosition;
    [Export] public Node2D? JumpFallPosition;
    [Export] public Node2D? FallHeightPosition;
    
    public override async Task PlayCinematic(
        CinematicsContext cinematicsContext, 
        CancellationToken skipToken,
        CancellationToken cancellationToken
        )
    {
        await cinematicsContext.CinematicsMethods.AwaitUntilPlayerIsOnTheGroundUseCase.Execute(cancellationToken);
        
        PlayerView playerView = cinematicsContext.PlayerView;

        playerView.CanUpdateMovement = false;
        playerView.AnimationPlayer!.ProcessMode = ProcessModeEnum.Disabled;
        playerView.AnimatedSprite!.Play(PlayerAnimationState.Idle);
        
        await cinematicsContext.CinematicsMethods.PlayDialogueUseCase.Execute(
            cinematicsContext.GameConfiguration.DialoguesConfiguration!.Test!,
            skipToken,
            cancellationToken
        );
        
        GTweenSequenceBuilder sequenceBuilder = GTweenSequenceBuilder.New();

        sequenceBuilder.AppendTime(1f)
            .AppendCallback(() => playerView.AnimatedSprite!.FlipH = false)
            .AppendTime(1f)
            .AppendCallback(() => playerView.AnimatedSprite!.Play(PlayerAnimationState.Run))
            .Append(playerView.TweenGlobalPositionX(PositionBeforeJump!.GlobalPosition.X, WalkTowardsJumpDuration)
                .SetEasing(Easing.Linear))
            .AppendCallback(() => playerView.AnimatedSprite!.Play(PlayerAnimationState.Idle))
            .AppendTime(0.5f)
            .AppendCallback(() => playerView.AnimatedSprite!.FlipH = true)
            .AppendTime(0.5f)
            .AppendCallback(() => playerView.AnimatedSprite!.FlipH = false)
            .Append(playerView.TweenGlobalPositionX(JumpFallPosition!.GlobalPosition.X, 1f).SetEasing(Easing.OutQuad))
            .JoinSequence(s => s
                .Append(playerView.TweenGlobalPositionY(JumpHeightPosition!.GlobalPosition.Y, 0.5f)
                    .SetEasing(Easing.OutQuad))
                .JoinCallback(() => playerView.AnimatedSprite!.Play(PlayerAnimationState.Jump))
                .Append(playerView.TweenGlobalPositionY(FallHeightPosition!.GlobalPosition.Y, 0.5f)
                    .SetEasing(Easing.InQuad))
                .JoinCallback(() => playerView.AnimatedSprite!.Play(PlayerAnimationState.Fall))
            )
            .AppendTime(0.5f);
        
        GTween tween = sequenceBuilder.Build();

        skipToken.Register(tween.Complete);
        
        await tween.PlayAsync(cancellationToken);
        
        playerView.CanUpdateMovement = true;
        playerView.AnimationPlayer!.ProcessMode = ProcessModeEnum.Inherit;
    }
}