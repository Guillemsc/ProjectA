using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Cinematics.Contexts;
using Game.GameContext.Cinematics.Views;
using Game.GameContext.Dialogues.Configurations;
using Game.GameContext.Npcs.Enums;
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

public partial class Intro2CinematicView : CinematicView
{
    [Export] public DialogueConfiguration? DialogueConfiguration;
    
    [Export] public SomeoneNpcView? SomeoneNpcView;
    
    [Export] public Node2D? PositionBeforeJump;
    [Export] public float WalkTowardsJumpDuration = 1f;
    
    [Export] public Node2D? JumpHeightPosition;
    [Export] public Node2D? JumpFallPosition;
    [Export] public Node2D? FallHeightPosition;
    
    public override async Task Play(
        CinematicContext context, 
        CancellationToken skipToken,
        CancellationToken cancellationToken
        )
    {
        PlayerView playerView = context.PlayerView;
        
        SomeoneNpcView!.AnimatedSprite!.FlipH = true;
        SomeoneNpcView.AnimatedSprite.Play(NpcAnimationState.Idle);
        
        playerView.CanUpdateMovement = false;
        playerView.AnimationPlayer!.ProcessMode = ProcessModeEnum.Disabled;
        
        await context.Methods.PlayDialogueUseCase.Execute(
            DialogueConfiguration!,
            skipToken,
            cancellationToken
        );
        
        GTweenSequenceBuilder sequenceBuilder = GTweenSequenceBuilder.New();

        sequenceBuilder
            .Append(SomeoneJumpDown())
            .AppendCallback(() => playerView.AnimatedSprite!.Play(PlayerAnimationState.Idle))
            .Append(playerView.TweenGlobalPositionY(17, 0.1f))
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
            .Append(playerView.TweenGlobalPositionX(JumpFallPosition!.GlobalPosition.X, 1f)
                .SetEasing(Easing.OutQuad))
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

    GTween SomeoneJumpDown()
    {
        return GTweenSequenceBuilder.New()
            .Append(SomeoneNpcView!.TweenGlobalPositionX(JumpFallPosition!.GlobalPosition.X, 1f)
                .SetEasing(Easing.OutQuad))
            .JoinSequence(s => s
                .Append(SomeoneNpcView!.TweenGlobalPositionY(JumpHeightPosition!.GlobalPosition.Y, 0.5f)
                    .SetEasing(Easing.OutQuad))
                .JoinCallback(() => SomeoneNpcView!.AnimatedSprite!.Play(NpcAnimationState.Jump))
                .Append(SomeoneNpcView!.TweenGlobalPositionY(FallHeightPosition!.GlobalPosition.Y, 0.5f)
                    .SetEasing(Easing.InQuad))
                .JoinCallback(() => SomeoneNpcView!.AnimatedSprite!.Play(NpcAnimationState.Fall))
            )
            .Build();
    }
}