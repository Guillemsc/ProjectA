using System;
using Game.GameContext.Crates.Enums;
using Godot;
using GUtils.AnimationGraphs;
using GUtils.Predicates;
using GUtilsGodot.AnimationGraphs;

namespace Game.GameContext.Crates.AnimationGraphPlayers;

public partial class CrateAnimationGraphPlayer : AnimationGraphPlayerNode
{
    [Export] public AnimatedSprite2D? AnimatedSprite2D;
    [Export] public AnimationPlayer? AnimationPlayer;
    
    public bool NeedsToPlayHit;
    public bool Broken;

    public event Action? SpawnContents;
    
    protected override IAnimationGraphPlayer BuildPlayer()
    {
        AnimationGraphNode idle = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            CreateAnimationState.Idle.ToString()
        ));
        
        AnimationGraphNode hit = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            CreateAnimationState.Hit.ToString()
        ));

        AnimationGraphNode broken = new AnimationGraphNode(new AnimationPlayerAnimationGraphBehaviour(
            AnimationPlayer!,
            CreateAnimationState.Broken.ToString()
        ));
        broken.OnEnterActions.Add(() => SpawnContents?.Invoke());
        
        AnimationGraphConnection idleToHit = idle.ConnectToUnsafe(hit);
        idleToHit.Conditions.Add(new CallbackPredicate(() => NeedsToPlayHit));
        idleToHit.OnSetActions.Add(() => NeedsToPlayHit = false);
        
        AnimationGraphConnection hitToIdle = hit.ConnectToUnsafe(idle);
        hitToIdle.Conditions.Add(new CallbackPredicate(() => !Broken));
        hitToIdle.WaitForFullExecution = true;
        
        AnimationGraphConnection hitToBroken = hit.ConnectToUnsafe(broken);
        hitToBroken.Conditions.Add(new CallbackPredicate(() => Broken));
        hitToBroken.WaitForFullExecution = true;
        
        AnimationGraphPlayer animationGraphPlayer = new AnimationGraphPlayer(idle);

        return animationGraphPlayer;
    }
}