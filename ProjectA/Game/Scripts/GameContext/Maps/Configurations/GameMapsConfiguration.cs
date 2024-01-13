using Godot;
using Godot.Collections;

namespace Game.GameContext.Maps.Configurations;

[GlobalClass]
public partial class GameMapsConfiguration : Resource
{
    [Export(PropertyHint.File, "*.tscn,*.scn")] public string? TestMap;
    [Export] public Array<MapConfiguration> MapConfigurations = new();
}