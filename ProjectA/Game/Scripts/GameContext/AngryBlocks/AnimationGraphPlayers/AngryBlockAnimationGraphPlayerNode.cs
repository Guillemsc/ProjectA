using Game.GameContext.AngryBlocks.Enums;
using Godot;
using GUtils.AnimationGraphs;
using GUtils.Predicates;
using GUtilsGodot.AnimationGraphs;

namespace Game.GameContext.AngryBlocks.AnimationGraphPlayers;

public partial class AngryBlockAnimationGraphPlayerNode : AnimationGraphPlayerNode
{
    [Export] public AnimatedSprite2D? AnimatedSprite2D;
    
    public bool NeedsToPlayBlink;
    public bool NeedsToPlayLeftHit;
    public bool NeedsToPlayRightHit;
    public bool NeedsToPlayTopHit;
    public bool NeedsToPlayBottomHit;
    
    protected override IAnimationGraphPlayer BuildPlayer()
    {
        AnimationGraphNode idle = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            AngryBlockAnimationState.Idle.ToString()
        ));
        
        AnimationGraphNode blink = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            AngryBlockAnimationState.Blink.ToString()
        ));
        
        AnimationGraphNode leftHit = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            AngryBlockAnimationState.LeftHit.ToString()
        ));
        
        AnimationGraphNode rightHit = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            AngryBlockAnimationState.RightHit.ToString()
        ));
        
        AnimationGraphNode topHit = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            AngryBlockAnimationState.TopHit.ToString()
        ));
        
        AnimationGraphNode bottomHit = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            AngryBlockAnimationState.BottomHit.ToString()
        ));
        
        AnimationGraphConnection idleToBlink = idle.ConnectToUnsafe(blink);
        idleToBlink.Conditions.Add(new CallbackPredicate(() => NeedsToPlayBlink));
        
        AnimationGraphConnection idleToLeftHit = idle.ConnectToUnsafe(leftHit);
        idleToLeftHit.Conditions.Add(new CallbackPredicate(() => NeedsToPlayLeftHit));
        
        AnimationGraphConnection idleToRightHit = idle.ConnectToUnsafe(rightHit);
        idleToRightHit.Conditions.Add(new CallbackPredicate(() => NeedsToPlayRightHit));
        
        AnimationGraphConnection idleToTopHit = idle.ConnectToUnsafe(topHit);
        idleToTopHit.Conditions.Add(new CallbackPredicate(() => NeedsToPlayTopHit));
        
        AnimationGraphConnection idleToBottomHit = idle.ConnectToUnsafe(bottomHit);
        idleToBottomHit.Conditions.Add(new CallbackPredicate(() => NeedsToPlayBottomHit));
        
        AnimationGraphConnection blinkToIdle = blink.ConnectToUnsafe(idle);
        blinkToIdle.OnSetActions.Add(() => NeedsToPlayBlink = false);
        blinkToIdle.WaitForFullExecution = true;
        
        AnimationGraphConnection blinkToLeftHit = blink.ConnectToUnsafe(leftHit);
        blinkToLeftHit.Conditions.Add(new CallbackPredicate(() => NeedsToPlayLeftHit));
        
        AnimationGraphConnection blinkToRightHit = blink.ConnectToUnsafe(rightHit);
        blinkToRightHit.Conditions.Add(new CallbackPredicate(() => NeedsToPlayRightHit));
        
        AnimationGraphConnection blinkToTopHit = blink.ConnectToUnsafe(topHit);
        blinkToTopHit.Conditions.Add(new CallbackPredicate(() => NeedsToPlayTopHit));
        
        AnimationGraphConnection blinkToBottomHit = blink.ConnectToUnsafe(bottomHit);
        blinkToBottomHit.Conditions.Add(new CallbackPredicate(() => NeedsToPlayBottomHit));
        
        AnimationGraphConnection leftHitToIdle = leftHit.ConnectToUnsafe(idle);
        leftHitToIdle.OnSetActions.Add(() => NeedsToPlayLeftHit = false);
        leftHitToIdle.WaitForFullExecution = true;
        
        AnimationGraphConnection rightHitToIdle = rightHit.ConnectToUnsafe(idle);
        rightHitToIdle.OnSetActions.Add(() => NeedsToPlayRightHit = false);
        rightHitToIdle.WaitForFullExecution = true;
        
        AnimationGraphConnection topHitToIdle = topHit.ConnectToUnsafe(idle);
        topHitToIdle.OnSetActions.Add(() => NeedsToPlayTopHit = false);
        topHitToIdle.WaitForFullExecution = true;
        
        AnimationGraphConnection bottomHitToIdle = bottomHit.ConnectToUnsafe(idle);
        bottomHitToIdle.OnSetActions.Add(() => NeedsToPlayBottomHit = false);
        bottomHitToIdle.WaitForFullExecution = true;
        
        AnimationGraphPlayer animationGraphPlayer = new AnimationGraphPlayer(idle);

        return animationGraphPlayer;
    }
}