using System.Collections.Generic;
using Godot;

namespace Game.GameContext.Players.Datas;

public sealed class PlayerCollidingNodesData
{
    public HashSet<Node2D> CollidingNodes = new();
    public HashSet<Node2D> CheckingCollidingNodes = new();
    public HashSet<Node2D> AlreadyCheckedCollidingNodes = new();
}