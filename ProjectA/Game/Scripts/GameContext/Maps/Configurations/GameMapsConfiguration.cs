using Godot;

namespace Game.GameContext.Maps.Configurations;

[GlobalClass]
public partial class GameMapsConfiguration : Resource
{
    [Export(PropertyHint.File, "*.tscn,*.scn")] public string? TestMap;
}