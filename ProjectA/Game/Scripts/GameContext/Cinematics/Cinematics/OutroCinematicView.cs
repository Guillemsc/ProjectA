using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Cinematics.Contexts;
using Game.GameContext.Cinematics.Views;
using Game.GameContext.Dialogues.Configurations;
using Game.GameContext.Npcs.Enums;
using Game.GameContext.Npcs.Views;
using Game.GameContext.Players.Views;
using Godot;
using GTweens.Builders;
using GTweens.Easings;
using GTweensGodot.Extensions;
using GUtils.Extensions;
using GUtils.Time.Extensions;
using GUtilsGodot.Extensions;
using GUtilsGodot.Time.Extensions;

namespace Game.GameContext.Cinematics.Cinematics;

public partial class OutroCinematicView : CinematicView
{
    [Export] public AnimationPlayer? AnimationPlayer;
    [Export] public DialogueConfiguration? Dialogue1Configuration;
    [Export] public DialogueConfiguration? Dialogue2Configuration;
    [Export] public DialogueConfiguration? Dialogue3Configuration;
    [Export] public DialogueConfiguration? Dialogue4Configuration;
    
    [Export] public SomeoneNpcView? SomeoneNpcView;

    [Export] public Node2D? CameraFocusPosition;
    [Export] public Node2D? SomeoneFlyPosition1;
    [Export] public Node2D? SomeoneFlyPosition2;
    [Export] public Node2D? SomeoneFlyPosition3;

    [Export] public Curve? FlyToPosition1Curve;
    [Export] public Curve? FlyToPosition3Curve;
    
    public override async Task Play(
        CinematicContext context, 
        CancellationToken skipToken,
        CancellationToken cancellationToken
        )
    {
        PlayerView playerView = context.PlayerView;
        
        SomeoneNpcView!.AnimatedSprite!.Play(NpcAnimationState.Idle);
        
        playerView.CanMove = false;
        
        context.Methods.SetCameraTargetUseCase.Execute(CameraFocusPosition!, false);

        await context.Services.GameTimesService.TimeContext.NewStartedTimer()
            .GodotAwaitSpan(0.5f.ToSeconds(), cancellationToken);
        
        await context.Methods.PlayDialogueUseCase.Execute(
            Dialogue1Configuration!,
            skipToken,
            cancellationToken
        );
        
        SomeoneNpcView!.AnimatedSprite!.FlipH = true;
        
        GTweenSequenceBuilder sequenceBuilder = GTweenSequenceBuilder.New();

        sequenceBuilder
            .Append(SomeoneNpcView.TweenGlobalPositionX(SomeoneFlyPosition1!.GlobalPosition.X, 2f))
            .Join(SomeoneNpcView.TweenGlobalPositionY(SomeoneFlyPosition1!.GlobalPosition.Y, 2f)
                .SetEasing(FlyToPosition1Curve!)
            );
        
        await sequenceBuilder.Build().PlayAsync(skipToken, cancellationToken);
        
        await context.Methods.PlayDialogueUseCase.Execute(
            Dialogue2Configuration!,
            skipToken,
            cancellationToken
        );
        
        GTweenSequenceBuilder sequenceBuilder2 = GTweenSequenceBuilder.New();

        sequenceBuilder2
            .Append(SomeoneNpcView.TweenGlobalPositionX(SomeoneFlyPosition2!.GlobalPosition.X, 2f))
            .Join(SomeoneNpcView.TweenGlobalPositionY(SomeoneFlyPosition2!.GlobalPosition.Y, 2f)
                .SetEasing(FlyToPosition1Curve!)
            );
        
        await sequenceBuilder2.Build().PlayAsync(skipToken, cancellationToken);
        
        await context.Methods.PlayDialogueUseCase.Execute(
            Dialogue3Configuration!,
            skipToken,
            cancellationToken
        );
        
        GTweenSequenceBuilder sequenceBuilder3 = GTweenSequenceBuilder.New();

        sequenceBuilder3
            .Append(SomeoneNpcView.TweenGlobalPositionX(SomeoneFlyPosition3!.GlobalPosition.X, 5f)
                .SetEasing(FlyToPosition3Curve!))
            .Join(SomeoneNpcView.TweenGlobalPositionY(SomeoneFlyPosition3!.GlobalPosition.Y, 5f)
                .SetEasing(Easing.InOutQuad))
            .JoinSequence(s => s
                .Append(SomeoneNpcView.TweenModulate(new Color(1.4f, 1.4f, 1.4f), 1f))
                .Append(SomeoneNpcView.TweenModulateAlpha(0f, 2f))
            );
        
        await sequenceBuilder3.Build().PlayAsync(skipToken, cancellationToken);
        
        await context.Methods.PlayDialogueUseCase.Execute(
            Dialogue4Configuration!,
            skipToken,
            cancellationToken
        );
        
        context.Methods.SetPlayerAsCameraTargetUseCase.Execute(false);
        
        playerView.CanUpdateMovement = true;
        playerView.AnimationPlayer!.ProcessMode = ProcessModeEnum.Inherit;
    }
}