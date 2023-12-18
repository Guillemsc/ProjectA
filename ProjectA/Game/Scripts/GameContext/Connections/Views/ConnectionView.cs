using Godot;
using GUtilsGodot.Rects.Nodes;

namespace Game.GameContext.Connections.Views;

public partial class ConnectionView : Node2D
{
    [Export] public Rect2dNode? Bounds;
    [Export(PropertyHint.File, "*.tscn,*.scn")] public string? Map;
    [Export] public string? SpawnId;
}