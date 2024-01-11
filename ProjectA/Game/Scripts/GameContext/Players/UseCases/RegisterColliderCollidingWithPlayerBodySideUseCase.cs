using Game.GameContext.Players.Datas;
using Godot;
using GUtils.Directions;

namespace Game.GameContext.Players.UseCases;

public sealed class RegisterColliderCollidingWithPlayerBodySideUseCase
{
    readonly PlayerBodyCollidingNodesOnSidesData _playerBodyCollidingNodesOnSidesData;

    public RegisterColliderCollidingWithPlayerBodySideUseCase(
        PlayerBodyCollidingNodesOnSidesData playerBodyCollidingNodesOnSidesData
        )
    {
        _playerBodyCollidingNodesOnSidesData = playerBodyCollidingNodesOnSidesData;
    }

    public void Execute(Node2D node2D, KinematicCollision2D kinematicCollision2D)
    {
        Vector2 collisionNormal = kinematicCollision2D.GetNormal();

        bool onTop = collisionNormal is { X: 0f, Y: < 0f };
        bool onBottom = collisionNormal is { X: 0f, Y: > 0f };
        bool onLeft = collisionNormal is { X: < 1f, Y: 0f };
        bool onRight = collisionNormal is { X: > 1f, Y: 0f };

        CardinalDirection direction = CardinalDirection.Down;

        if (onTop)
        {
            direction = CardinalDirection.Up;
        }
        else if (onBottom)
        {
            direction = CardinalDirection.Down;
        }
        else if (onLeft)
        {
            direction = CardinalDirection.Left;
        }
        else if (onRight)
        {
            direction = CardinalDirection.Right;
        }
        
        GD.Print(direction);

        _playerBodyCollidingNodesOnSidesData.CollidingNodes[direction] = node2D;
    }
}