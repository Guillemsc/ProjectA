using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Cinematics.Contexts;
using Game.GameContext.Cinematics.Views;
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
    [Export] public Node2D? MovePosition1;
    
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
        
        GTweenSequenceBuilder sequenceBuilder = GTweenSequenceBuilder.New();

        sequenceBuilder
            .AppendCallback(() => playerView!.AnimatedSprite!.Play(PlayerAnimationState.Idle))
            .Append(playerView.TweenGlobalPositionX(MovePosition1!.GlobalPosition.X, 3)
                .SetEasing(Easing.Linear));
        
        await sequenceBuilder.Build().PlayAsync(skipToken, cancellationToken);
        
        playerView.CanUpdateMovement = true;
        playerView.AnimationPlayer!.ProcessMode = ProcessModeEnum.Inherit;
    }
}