using Game.GameContext.Areas.Views;
using Game.GameContext.Cinematics.Views;
using Game.GameContext.Connections.Views;
using Godot;

namespace Game.GameContext.Maps.Views;

public partial class MapView : Node2D
{
    [Export] public string ReadableName = "Placeholder";
    [Export] public AreaView[]? AreaViews;
    [Export] public ConnectionView[]? ConnectionViews;
    [Export] public CinematicView? OptionalStartingCinematic;
}