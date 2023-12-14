using Godot;

namespace Game.GameContext.Maps.Configurations;

[GlobalClass]
public partial class GameMapsConfiguration : Resource
{
    [Export] public PackedScene[]? MapsPrefabs;
}