using Godot;

namespace Game.GameContext.Player.Configurations;

[GlobalClass]
public partial class GamePlayerConfiguration : Resource
{
    [Export] public PackedScene? PlayerPrefab;
    [Export] public float HorizontalAcceleration = 300.0f;
    [Export] public float HorizontalDeceleration = 300.0f;
    [Export] public float HorizontalMaxSpeed = 300.0f;
    [Export] public float JumpVelocity = 400.0f;
    [Export] public float Gravity = 890.0f;
    [Export] public float VerticalMaxFallSpeed = 300.0f;
    [Export] public float VerticalMaxFallSpeedOnWall = 100.0f;
}