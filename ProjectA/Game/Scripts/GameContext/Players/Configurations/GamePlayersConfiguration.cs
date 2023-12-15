using Godot;

namespace Game.GameContext.Players.Configurations;

[GlobalClass]
public partial class GamePlayersConfiguration : Resource
{
    [Export] public PackedScene? PlayerPrefab;
    [Export] public float HorizontalAcceleration = 300.0f;
    [Export] public float HorizontalDeceleration = 300.0f;
    [Export] public float HorizontalMaxSpeed = 300.0f;
    [Export] public float HorizontalMaxSpeedOnAir = 300.0f;
    [Export] public float JumpVelocity = 400.0f;
    [Export] public float JumpsResetMinSeconds = 0.1f;
    [Export] public float Gravity = 890.0f;
    [Export] public float VerticalMaxFallSpeed = 300.0f;
    [Export] public float VerticalMaxFallSpeedOnWall = 100.0f;
    [Export] public float UncontrolledHorizontalJumpVelocityMultiplier = 0.4f;
}