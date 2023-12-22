using Godot;

namespace Game.GameContext.Cinematics.Views;

public partial class CinematicTriggerView : Area2D
{
    [Export] public CinematicView? CinematicView;
}