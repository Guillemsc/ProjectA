using Godot;

namespace Game.GameContext.Collectables.Configurations;

[GlobalClass]
public partial class GameCollectablesConfiguration : Resource
{
    [Export] public PackedScene? ApplePrefab;
}