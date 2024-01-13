using Godot;

namespace Game.GameContext.Maps.Configurations;

[GlobalClass]
public partial class MapConfiguration : Resource
{
    [Export(PropertyHint.File, "*.tscn,*.scn")] public string? Map;
}