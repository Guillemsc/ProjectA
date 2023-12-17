using Game.GameContext.VelocityBoosters.Enums;
using Godot;
using GUtils.AnimationGraphs;
using GUtils.Predicates;
using GUtilsGodot.AnimationGraphs;

namespace Game.GameContext.VelocityBoosters.AnimationGraphPlayers;

public partial class VelocityBoosterAnimationGraphPlayer : AnimationGraphPlayerNode
{
    [Export] public AnimatedSprite2D? AnimatedSprite2D;
    
    public bool NeedsToPlayCollected;

    protected override IAnimationGraphPlayer BuildPlayer()
    {
        AnimationGraphNode idle = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            VelocityBoostersAnimationState.Idle.ToString()
        ));
        
        AnimationGraphNode collected = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            VelocityBoostersAnimationState.Collected.ToString()
        ));
        
        AnimationGraphNode hidden = new AnimationGraphNode(new ActionAnimationGraphBehaviour(
            () => AnimatedSprite2D!.Visible = false
        ));
        
        AnimationGraphConnection idleToCollected = idle.ConnectToUnsafe(collected);
        idleToCollected.Conditions.Add(new CallbackPredicate(() => NeedsToPlayCollected));
        idleToCollected.OnSetActions.Add(() => NeedsToPlayCollected = false);
        
        AnimationGraphConnection collectedToHidden = collected.ConnectToUnsafe(hidden);
        collectedToHidden.WaitForFullExecution = true;
        
        AnimationGraphPlayer animationGraphPlayer = new AnimationGraphPlayer(idle);

        return animationGraphPlayer;
    }
}