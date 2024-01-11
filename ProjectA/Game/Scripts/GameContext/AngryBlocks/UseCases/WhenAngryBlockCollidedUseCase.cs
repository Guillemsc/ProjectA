using System;
using Game.GameContext.AngryBlocks.Views;
using GUtils.Directions;
using GUtils.Extensions;

namespace Game.GameContext.AngryBlocks.UseCases;

public sealed class WhenAngryBlockCollidedUseCase
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

        switch (cardinalDirection)
        {
            case CardinalDirection.Left:
            {
                angryBlockView.AnimationGraphPlayer!.NeedsToPlayLeftHit = true;
                break;
            }
            case CardinalDirection.Up:
            {
                angryBlockView.AnimationGraphPlayer!.NeedsToPlayTopHit = true;
                break;
            }
            case CardinalDirection.Right:
            {
                angryBlockView.AnimationGraphPlayer!.NeedsToPlayRightHit = true;
                break;
            }
            case CardinalDirection.Down:
            {
                angryBlockView.AnimationGraphPlayer!.NeedsToPlayBottomHit = true;
                break;
            }
        }
        
        angryBlockView.CurrentMovementSpeed = 0;
        angryBlockView.CurrentMovementSequenceIndex = angryBlockView.MovementSequence!.GetNextOrSmallestIndex(
            angryBlockView.CurrentMovementSequenceIndex
        );
    }
}