using System.Collections.Generic;
using Godot;
using GUtils.Directions;

namespace Game.GameContext.Players.Datas;

public sealed class PlayerBodyCollidingNodesOnSidesData
{
    public Dictionary<CardinalDirection, Node2D> CollidingNodes = new();
}