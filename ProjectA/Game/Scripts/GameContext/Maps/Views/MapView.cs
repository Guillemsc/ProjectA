using Game.GameContext.Areas.Views;
using Game.GameContext.Connections.Views;
using Godot;

namespace Game.GameContext.Maps.Views;

public partial class MapView : Node2D
{
    [Export] public AreaView[]? AreaViews;
    [Export] public ConnectionView[]? ConnectionViews;
}