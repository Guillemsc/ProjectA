using Godot;

namespace Game.GameContext.AngryBlocks.Configurations;

[GlobalClass]
public partial class AngryBlockMovementConfiguration : Resource
{
    [Export] public float Acceleration = 5;
    [Export] public float MaxSpeed = 100;
}