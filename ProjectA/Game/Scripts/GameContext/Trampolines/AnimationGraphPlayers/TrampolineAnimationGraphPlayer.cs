using Game.GameContext.Trampolines.Enums;
using Godot;
using GUtils.AnimationGraphs;
using GUtils.Predicates;
using GUtilsGodot.AnimationGraphs;

namespace Game.GameContext.Trampolines.AnimationGraphPlayers;

public partial class TrampolineAnimationGraphPlayer : AnimationGraphPlayerNode
{
    [Export] public AnimatedSprite2D? AnimatedSprite2D;
    
    public bool NeedsToPlayJump;

    protected override IAnimationGraphPlayer BuildPlayer()
    {
        AnimationGraphNode idle = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            TrampolineAnimationState.Idle.ToString()
        ));
        
        AnimationGraphNode jump = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            TrampolineAnimationState.Jump.ToString()
        ));
        
        AnimationGraphConnection idleToJump = idle.ConnectToUnsafe(jump);
        idleToJump.Conditions.Add(new CallbackPredicate(() => NeedsToPlayJump));
        idleToJump.OnSetActions.Add(() => NeedsToPlayJump = false);
        
        AnimationGraphConnection jumpToIdle = jump.ConnectToUnsafe(idle);
        jumpToIdle.Conditions.Add(new CallbackPredicate(() => NeedsToPlayJump || jump.Behaviour.Completed));
        
        AnimationGraphPlayer animationGraphPlayer = new AnimationGraphPlayer(idle);

        return animationGraphPlayer;
    }
}