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

public partial class IntroCinematicView : CinematicView
{
    [Export] public DialogueConfiguration? Dialogue1Configuration;
    [Export] public DialogueConfiguration? Dialogue2Configuration;
    
    [Export] public SomeoneNpcView? SomeoneNpcView;

    [Export] public AnimationPlayer? AnimationPlayer;
    [Export] public AudioStream? AudioStream;

    [Export] public float RunDuration = 1f;
    [Export] public Node2D? PositionToRun;
    
    public override async Task Play(
        CinematicContext context, 
        CancellationToken skipToken,
        CancellationToken cancellationToken
        )
    {
        context.Services.MusicService.Play(AudioStream!, -10);
        
        PlayerView playerView = context.PlayerView;
        
        SomeoneNpcView!.AnimatedSprite!.FlipH = true;
        SomeoneNpcView.AnimatedSprite.Play(NpcAnimationState.Sit);
        
        playerView.CanUpdateMovement = false;
        playerView.AnimationPlayer!.ProcessMode = ProcessModeEnum.Disabled;
        playerView.AnimatedSprite!.Play(PlayerAnimationState.Sit);
        playerView.SetPositionY(11);
        
        await AnimationPlayer!.PlayAndAwaitCompletition("Intro", skipToken, cancellationToken);
        
        await context.Methods.PlayDialogueUseCase.Execute(
            Dialogue1Configuration!,
            skipToken,
            cancellationToken
        );
        
        GTweenSequenceBuilder turnToAnimalSequence = GTweenSequenceBuilder.New();

        float surpriseTopPosition = SomeoneNpcView.Position.Y - 10;
        float surpriseBottomPosition = SomeoneNpcView.Position.Y;

        turnToAnimalSequence
            .AppendTime(0.2f)
            .AppendCallback(() => SomeoneNpcView!.AnimatedSprite!.FlipH = false)
            .AppendTime(0.2f)
            .Append(SomeoneNpcView!.TweenPositionY(surpriseTopPosition, 0.15f)
                .SetEasing(Easing.OutQuad))
            .Append(SomeoneNpcView!.TweenPositionY(surpriseBottomPosition, 0.15f)
                .SetEasing(Easing.InQuad))
            .AppendTime(0.2f);

        await turnToAnimalSequence.Build().PlayAsync(skipToken, cancellationToken);
        
        await context.Methods.PlayDialogueUseCase.Execute(
            Dialogue2Configuration!,
            skipToken,
            cancellationToken
        );
        
        GTweenSequenceBuilder sequenceBuilder = GTweenSequenceBuilder.New();

        sequenceBuilder
            .AppendCallback(() => SomeoneNpcView!.AnimatedSprite!.Play(PlayerAnimationState.Idle))
            .Append(SomeoneNpcView!.TweenGlobalPositionY(17, 0.1f))
            .AppendTime(0.3f)
            .AppendCallback(() => SomeoneNpcView!.AnimatedSprite!.Play(NpcAnimationState.Idle))
            .Append(SomeoneNpcView!.TweenGlobalPositionX(PositionToRun!.GlobalPosition.X, RunDuration)
                .SetEasing(Easing.Linear))
            .AppendCallback(() => SomeoneNpcView.Visible = false);
        
        await sequenceBuilder.Build().PlayAsync(skipToken, cancellationToken);
        
        context.Services.MusicService.Stop();
        
        playerView.CanUpdateMovement = true;
        playerView.AnimationPlayer!.ProcessMode = ProcessModeEnum.Inherit;
    }
}