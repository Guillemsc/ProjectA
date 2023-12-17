using Godot;

namespace Game.GameContext.Crates.Configurations;

[GlobalClass]
public partial class GameCratesConfiguration : Resource
{
    [Export] public float VerticalFruitSpawnForce = 20000;
    [Export] public float HorizontalFruitSpawnForce = 5000;
}