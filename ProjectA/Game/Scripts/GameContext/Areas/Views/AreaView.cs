using Godot;
using GUtilsGodot.Rects.Nodes;

namespace Game.GameContext.Areas.Views;

public partial class AreaView : Node2D
{
    [Export] public Rect2dNode? Bounds;
    [Export] public float CameraZoom = 2f;
}