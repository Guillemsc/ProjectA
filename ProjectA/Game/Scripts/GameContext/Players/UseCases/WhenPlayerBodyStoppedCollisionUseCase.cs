using Game.GameContext.Players.Datas;
using Godot;

namespace Game.GameContext.Players.UseCases;

public sealed class WhenPlayerBodyStoppedCollisionUseCase
{
    readonly PlayerBodyCollidingNodesOnSidesData _playerBodyCollidingNodesOnSidesData;

    public WhenPlayerBodyStoppedCollisionUseCase(
        PlayerBodyCollidingNodesOnSidesData playerBodyCollidingNodesOnSidesData
        )
    {
        _playerBodyCollidingNodesOnSidesData = playerBodyCollidingNodesOnSidesData;
    }

    public void Execute(Node2D node, KinematicCollision2D kinematicCollision2D)
    {
        
    }
}