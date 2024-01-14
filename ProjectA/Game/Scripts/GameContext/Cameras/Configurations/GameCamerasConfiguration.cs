using Godot;

namespace Game.GameContext.Cameras.Configurations;

[GlobalClass]
public partial class GameCamerasConfiguration : Resource
{
    [Export] public float FollowTargetSpeed = 5f;
    [Export] public float FollowZoomSpeed = 5f;
    [Export] public float LowShakeStrenght = 2f;
    [Export] public float MiediumShakeStrenght = 5f;
    [Export] public float StrongShakeStrenght = 7f;
    [Export] public float ShakeAttenuation = 5f;
}