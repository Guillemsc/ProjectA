using Game.GameContext.AngryBlocks.Enums;
using Game.GameContext.Saws.Enums;
using Godot;
using GUtils.AnimationGraphs;
using GUtils.Predicates;
using GUtilsGodot.AnimationGraphs;

namespace Game.GameContext.Saws.AnimationGraphPlayers;

public partial class SawAnimationGraphPlayerNode : AnimationGraphPlayerNode
{
    [Export] public AnimatedSprite2D? AnimatedSprite2D;

    public bool On;
    
    protected override IAnimationGraphPlayer BuildPlayer()
    {
        AnimationGraphNode off = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            SawAnimationState.Off.ToString()
        ));
        
        AnimationGraphNode on = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            SawAnimationState.On.ToString()
        ));
        
        AnimationGraphConnection offToOn = off.ConnectToUnsafe(on);
        offToOn.Conditions.Add(new CallbackPredicate(() => On));
        
        AnimationGraphConnection onToOff = on.ConnectToUnsafe(off);
        onToOff.Conditions.Add(new CallbackPredicate(() => !On));
        
        AnimationGraphPlayer animationGraphPlayer = new AnimationGraphPlayer(on);

        return animationGraphPlayer;
    }
}