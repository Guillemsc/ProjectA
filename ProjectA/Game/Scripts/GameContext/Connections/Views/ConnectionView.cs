using Game.GameContext.Connections.Enums;
using Godot;
using GUtilsGodot.Rects.Nodes;

namespace Game.GameContext.Connections.Views;

public partial class ConnectionView : Node2D
{
    [Export] public string ReadableName = "Placeholder";
    [Export] public ConnectionType ConnectionType;
    [Export] public string Uid = string.Empty;
    [Export(PropertyHint.File, "*.tscn,*.scn")] public string? Map;
    [Export] public string? SpawnId;
    [Export] public Node2D? SpawnPosition;
    [Export] public Rect2dNode? Bounds;
}