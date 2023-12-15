using Godot;

namespace Game.GameContext.Crates.Views;

public partial class CrateView : Node2D
{
    [Export] public StaticBody2D? StaticBody2D;
    [Export] public float BounceStrenght = 300f;
}