using Godot;

namespace Game.General.Buttons.Configurations;

[GlobalClass]
public partial class GameMenuButtonConfiguration : Resource
{
    [Export] public float Offset = -10;
    [Export] public Curve? Curve;
    [Export] public float Duration = 0.5f;
}