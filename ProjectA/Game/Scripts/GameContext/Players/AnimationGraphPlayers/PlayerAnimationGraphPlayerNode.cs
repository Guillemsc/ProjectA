using Game.GameContext.Players.Enums;
using Godot;
using GUtils.AnimationGraphs;
using GUtils.Directions;
using GUtils.Locations.Enums;
using GUtils.Predicates;
using GUtilsGodot.AnimationGraphs;

namespace Game.GameContext.Players.AnimationGraphPlayers;

public partial class PlayerAnimationGraphPlayerNode : AnimationGraphPlayerNode
{
    [Export] public AnimatedSprite2D? AnimatedSprite2D;
    
    public bool OnAir;
    public PlayerOnAirState OnAirState;
   
    public bool OnWall;
    public HorizontalLocation OnWallLocation;
   
    public bool MovingHorizontally;
    public HorizontalDirection HorizontalDirection;
    
    public bool NeedsToPlayDoubleJump;
    
    protected override IAnimationGraphPlayer BuildPlayer()
    {
        AnimationGraphNode idle = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            PlayerAnimationState.Idle.ToString()
        ));
        
        AnimationGraphNode run = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            PlayerAnimationState.Run.ToString()
        ));
        
        AnimationGraphNode jump = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            PlayerAnimationState.Jump.ToString()
        ));

        AnimationGraphNode doubleJump = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            PlayerAnimationState.DoubleJump.ToString()
        ));
        
        AnimationGraphNode fall = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            PlayerAnimationState.Fall.ToString()
        ));
        
        AnimationGraphNode wall = new AnimationGraphNode(new AnimatedSprite2DAnimationGraphBehaviour(
            AnimatedSprite2D!,
            PlayerAnimationState.Wall.ToString()
        ));
        
        AnimationGraphConnection idleToRun = idle.ConnectToUnsafe(run);
        idleToRun.Conditions.Add(new CallbackPredicate(() => MovingHorizontally));
        idleToRun.Conditions.Add(new CallbackPredicate(() => !OnAir));
        
        AnimationGraphConnection idleToJump = idle.ConnectToUnsafe(jump);
        idleToJump.Conditions.Add(new CallbackPredicate(() => !MovingHorizontally));
        idleToJump.Conditions.Add(new CallbackPredicate(() => OnAir));
        idleToJump.Conditions.Add(new CallbackPredicate(() => OnAirState == PlayerOnAirState.Jump));
        
        AnimationGraphConnection idleToFall = idle.ConnectToUnsafe(fall);
        idleToFall.Conditions.Add(new CallbackPredicate(() => !MovingHorizontally));
        idleToFall.Conditions.Add(new CallbackPredicate(() => OnAir));
        idleToFall.Conditions.Add(new CallbackPredicate(() => OnAirState == PlayerOnAirState.Fall));
        
        AnimationGraphConnection runToIdle = run.ConnectToUnsafe(idle);
        runToIdle.Conditions.Add(new CallbackPredicate(() => !MovingHorizontally));
        runToIdle.Conditions.Add(new CallbackPredicate(() => !OnAir));
        
        AnimationGraphConnection runToJump = run.ConnectToUnsafe(jump);
        runToJump.Conditions.Add(new CallbackPredicate(() => OnAir));
        runToJump.Conditions.Add(new CallbackPredicate(() => OnAirState == PlayerOnAirState.Jump));
        
        AnimationGraphConnection runToFall = run.ConnectToUnsafe(fall);
        runToFall.Conditions.Add(new CallbackPredicate(() => OnAir));
        runToFall.Conditions.Add(new CallbackPredicate(() => OnAirState == PlayerOnAirState.Fall));
        
        AnimationGraphConnection jumpToIdle = jump.ConnectToUnsafe(idle);
        jumpToIdle.Conditions.Add(new CallbackPredicate(() => !MovingHorizontally));
        jumpToIdle.Conditions.Add(new CallbackPredicate(() => !OnAir));
        
        AnimationGraphConnection jumpToRun = jump.ConnectToUnsafe(run);
        jumpToRun.Conditions.Add(new CallbackPredicate(() => !OnAir));
        jumpToRun.Conditions.Add(new CallbackPredicate(() => MovingHorizontally));
        
        AnimationGraphConnection jumpToFall = jump.ConnectToUnsafe(fall);
        jumpToFall.Conditions.Add(new CallbackPredicate(() => OnAir));
        jumpToFall.Conditions.Add(new CallbackPredicate(() => OnAirState == PlayerOnAirState.Fall));
        
        AnimationGraphConnection jumpToWall = jump.ConnectToUnsafe(wall);
        jumpToWall.Conditions.Add(new CallbackPredicate(() => OnWall));
        jumpToWall.Conditions.Add(new CallbackPredicate(() => OnAir));
        
        AnimationGraphConnection jumpToDoubleJump = jump.ConnectToUnsafe(doubleJump);
        jumpToDoubleJump.Conditions.Add(new CallbackPredicate(() => NeedsToPlayDoubleJump));
        jumpToDoubleJump.Conditions.Add(new CallbackPredicate(() => OnAir));
        jumpToDoubleJump.OnSetActions.Add(() => NeedsToPlayDoubleJump = false);
        
        AnimationGraphConnection doubleJumpToJump = doubleJump.ConnectToUnsafe(jump);
        doubleJumpToJump.Conditions.Add(new CallbackPredicate(() => OnAir));
        doubleJumpToJump.Conditions.Add(new CallbackPredicate(() => !OnWall));
        doubleJumpToJump.Conditions.Add(new CallbackPredicate(() => OnAirState == PlayerOnAirState.Jump));
        doubleJumpToJump.WaitForFullExecution = true;
        
        AnimationGraphConnection doubleJumpToFall = doubleJump.ConnectToUnsafe(fall);
        doubleJumpToFall.Conditions.Add(new CallbackPredicate(() => OnAir));
        doubleJumpToFall.Conditions.Add(new CallbackPredicate(() => !OnWall));
        doubleJumpToFall.Conditions.Add(new CallbackPredicate(() => OnAirState == PlayerOnAirState.Fall));
        doubleJumpToFall.WaitForFullExecution = true;
        
        AnimationGraphConnection doubleJumpToWall = doubleJump.ConnectToUnsafe(wall);
        doubleJumpToWall.Conditions.Add(new CallbackPredicate(() => OnAir));
        doubleJumpToWall.Conditions.Add(new CallbackPredicate(() => OnWall));
        
        AnimationGraphConnection doubleJumpToIdle = doubleJump.ConnectToUnsafe(idle);
        doubleJumpToIdle.Conditions.Add(new CallbackPredicate(() => !MovingHorizontally));
        doubleJumpToIdle.Conditions.Add(new CallbackPredicate(() => !OnAir));
        
        AnimationGraphConnection doubleJumpToRun = doubleJump.ConnectToUnsafe(run);
        doubleJumpToRun.Conditions.Add(new CallbackPredicate(() => MovingHorizontally));
        doubleJumpToRun.Conditions.Add(new CallbackPredicate(() => !OnAir));
        
        AnimationGraphConnection fallToIdle = fall.ConnectToUnsafe(idle);
        fallToIdle.Conditions.Add(new CallbackPredicate(() => !MovingHorizontally));
        fallToIdle.Conditions.Add(new CallbackPredicate(() => !OnAir));
        
        AnimationGraphConnection fallToRun = fall.ConnectToUnsafe(run);
        fallToRun.Conditions.Add(new CallbackPredicate(() => !OnAir));
        fallToRun.Conditions.Add(new CallbackPredicate(() => MovingHorizontally));
        
        AnimationGraphConnection fallToWall = fall.ConnectToUnsafe(wall);
        fallToWall.Conditions.Add(new CallbackPredicate(() => OnWall));
        fallToWall.Conditions.Add(new CallbackPredicate(() => OnAir));
        
        AnimationGraphConnection fallToJump = fall.ConnectToUnsafe(jump);
        fallToJump.Conditions.Add(new CallbackPredicate(() => OnAir));
        fallToJump.Conditions.Add(new CallbackPredicate(() => OnAirState == PlayerOnAirState.Jump));
        
        AnimationGraphConnection fallToDoubleJump = fall.ConnectToUnsafe(doubleJump);
        fallToDoubleJump.Conditions.Add(new CallbackPredicate(() => NeedsToPlayDoubleJump));
        fallToDoubleJump.Conditions.Add(new CallbackPredicate(() => OnAir));
        fallToDoubleJump.OnSetActions.Add(() => NeedsToPlayDoubleJump = false);
        
        AnimationGraphConnection wallToIdle = wall.ConnectToUnsafe(idle);
        wallToIdle.Conditions.Add(new CallbackPredicate(() => !MovingHorizontally));
        wallToIdle.Conditions.Add(new CallbackPredicate(() => !OnAir));
        
        AnimationGraphConnection wallToJump = wall.ConnectToUnsafe(jump);
        wallToJump.Conditions.Add(new CallbackPredicate(() => !OnWall));
        wallToJump.Conditions.Add(new CallbackPredicate(() => OnAir));
        
        AnimationGraphPlayer animationGraphPlayer = new AnimationGraphPlayer(fall);

        return animationGraphPlayer;
    }
}