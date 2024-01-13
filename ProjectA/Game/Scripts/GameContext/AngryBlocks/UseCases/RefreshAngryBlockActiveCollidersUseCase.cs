using Game.GameContext.AngryBlocks.Views;
using Godot;
using GUtils.Directions;
using GUtils.Extensions;

namespace Game.GameContext.AngryBlocks.UseCases;

public sealed class RefreshAngryBlockActiveCollidersUseCase
{
    public void Execute(AngryBlockView angryBlockView)
    {
        bool couldGetCurrentDirection = angryBlockView.MovementSequence!.TryGet(
            angryBlockView.CurrentMovementSequenceIndex,
            out CardinalDirection cardinalDirection
        );

        if (!couldGetCurrentDirection)
        {
            return;
        }

        void Run()
        {
            angryBlockView.LeftWallDetectorArea2d!.ProcessMode = cardinalDirection == CardinalDirection.Left
                ? Node.ProcessModeEnum.Inherit
                : Node.ProcessModeEnum.Disabled;
        
            angryBlockView.RightWallDetectorArea2d!.ProcessMode = cardinalDirection == CardinalDirection.Right
                ? Node.ProcessModeEnum.Inherit
                : Node.ProcessModeEnum.Disabled;
        
            angryBlockView.TopWallDetectorArea2d!.ProcessMode = cardinalDirection == CardinalDirection.Up
                ? Node.ProcessModeEnum.Inherit
                : Node.ProcessModeEnum.Disabled;
        
            angryBlockView.BottomWallDetectorArea2d!.ProcessMode = cardinalDirection == CardinalDirection.Down
                ? Node.ProcessModeEnum.Inherit
                : Node.ProcessModeEnum.Disabled;
            
            angryBlockView.LeftPlayerKillerArea2d!.ProcessMode = cardinalDirection == CardinalDirection.Left
                ? Node.ProcessModeEnum.Inherit
                : Node.ProcessModeEnum.Disabled;
            
            angryBlockView.RightPlayerKillerArea2d!.ProcessMode = cardinalDirection == CardinalDirection.Right
                ? Node.ProcessModeEnum.Inherit
                : Node.ProcessModeEnum.Disabled;
            
            angryBlockView.TopPlayerKillerArea2d!.ProcessMode = cardinalDirection == CardinalDirection.Up
                ? Node.ProcessModeEnum.Inherit
                : Node.ProcessModeEnum.Disabled;
            
            angryBlockView.BottomPlayerKillerArea2d!.ProcessMode = cardinalDirection == CardinalDirection.Down
                ? Node.ProcessModeEnum.Inherit
                : Node.ProcessModeEnum.Disabled;
        }

        Callable.From(Run).CallDeferred();
    }
}