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

public partial class OutroCinematicView : CinematicView
{
    [Export] public SomeoneNpcView? SomeoneNpcView;
    
    public override async Task Play(
        CinematicContext context, 
        CancellationToken skipToken,
        CancellationToken cancellationToken
        )
    {
        PlayerView playerView = context.PlayerView;
        
        SomeoneNpcView!.AnimatedSprite!.FlipH = true;
        SomeoneNpcView.AnimatedSprite.Play(NpcAnimationState.Sit);
        
        playerView.CanUpdateMovement = false;
        playerView.AnimationPlayer!.ProcessMode = ProcessModeEnum.Disabled;
        playerView.AnimatedSprite!.Play(PlayerAnimationState.Sit);
        playerView.SetPositionY(11);
        
        playerView.CanUpdateMovement = true;
        playerView.AnimationPlayer!.ProcessMode = ProcessModeEnum.Inherit;
    }
}