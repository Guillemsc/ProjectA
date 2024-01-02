using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Cinematics.Contexts;
using Game.GameContext.Cinematics.Views;
using Game.GameContext.Npcs.Views;
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
    [Export] public SomeoneNpcView? SomeoneNpcView;

    [Export] public AudioStream? AudioStream;
    
    [Export] public Node2D? PositionBeforeJump;
    [Export] public float WalkTowardsJumpDuration = 1f;
    
    [Export] public Node2D? JumpHeightPosition;
    [Export] public Node2D? JumpFallPosition;
    [Export] public Node2D? FallHeightPosition;
    
    public override async Task PlayCinematic(
        CinematicsContext context, 
        CancellationToken skipToken,
        CancellationToken cancellationToken
        )
    {
        context.Services.MusicService.Play(AudioStream!);
        
        SomeoneNpcView!.AnimatedSprite!.FlipH = true;
        
        await context.Methods.AwaitUntilPlayerIsOnTheGroundUseCase.Execute(cancellationToken);
        
        PlayerView playerView = context.PlayerView;

        playerView.CanUpdateMovement = false;
        playerView.AnimationPlayer!.ProcessMode = ProcessModeEnum.Disabled;
        playerView.AnimatedSprite!.Play(PlayerAnimationState.Idle);
        
        await context.Methods.PlayDialogueUseCase.Execute(
            context.GameConfiguration.DialoguesConfiguration!.Test!,
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
        
        context.Services.MusicService.Stop();
        
        playerView.CanUpdateMovement = true;
        playerView.AnimationPlayer!.ProcessMode = ProcessModeEnum.Inherit;
    }
}