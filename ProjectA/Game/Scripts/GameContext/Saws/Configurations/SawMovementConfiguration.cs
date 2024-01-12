using Godot;

namespace Game.GameContext.Saws.Configurations;

[GlobalClass]
public partial class SawMovementConfiguration : Resource
{
    [Export] public bool Loops;
    [Export] public float Speed = 100;
}