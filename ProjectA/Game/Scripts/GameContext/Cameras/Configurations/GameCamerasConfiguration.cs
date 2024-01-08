using Godot;

namespace Game.GameContext.Cameras.Configurations;

[GlobalClass]
public partial class GameCamerasConfiguration : Resource
{
    [Export] public float FollowTargetSpeed = 5f;
    [Export] public float FollowZoomSpeed = 5f;
    [Export] public float ShakeStrenght = 5f;
    [Export] public float ShakeAttenuation = 5f;
}