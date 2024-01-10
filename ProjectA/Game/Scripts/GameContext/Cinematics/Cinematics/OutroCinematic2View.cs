using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Cinematics.Contexts;
using Game.GameContext.Cinematics.Views;
using Game.GameContext.Dialogues.Configurations;
using Game.GameContext.Players.Enums;
using Game.GameContext.Players.Views;
using Godot;
using GTweens.Builders;
using GTweens.Easings;
using GTweensGodot.Extensions;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Cinematics.Cinematics;

public partial class OutroCinematic2View : CinematicView
{
    [Export] public AnimationPlayer? AnimationPlayer;
    
    [Export] public DialogueConfiguration? Dialogue1Configuration;
    
    [Export] public Node2D? MovePosition1;
    
    [Export] public Node2D? CameraFocusPosition;
    
    public override async Task Play(
        CinematicContext context, 
        CancellationToken skipToken,
        CancellationToken cancellationToken
        )
    {
        PlayerView playerView = context.PlayerView;
        
        playerView.CanMove = false;
        playerView.AnimationPlayer!.ProcessMode = ProcessModeEnum.Disabled;
        playerView!.AnimatedSprite!.FlipH = false;
        
        context.Methods.SetCameraTargetUseCase.Execute(CameraFocusPosition!, false);
        
        GTweenSequenceBuilder sequenceBuilder = GTweenSequenceBuilder.New();

        sequenceBuilder
            .AppendCallback(() => playerView!.AnimatedSprite!.Play(PlayerAnimationState.Idle))
            .Append(playerView.TweenGlobalPositionX(MovePosition1!.GlobalPosition.X, 3)
                .SetEasing(Easing.Linear));
        
        await sequenceBuilder.Build().PlayAsync(skipToken, cancellationToken);
        
        await context.Methods.PlayDialogueUseCase.Execute(
            Dialogue1Configuration!,
            skipToken,
            cancellationToken
        );

        await AnimationPlayer!.PlayAndAwaitCompletition("CameraOutro", skipToken, cancellationToken);

        await context.Methods.PlayOutroUseCase.Execute(cancellationToken);
    }
}