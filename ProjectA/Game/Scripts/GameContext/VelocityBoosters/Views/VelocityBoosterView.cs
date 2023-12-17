using Game.GameContext.VelocityBoosters.AnimationGraphPlayers;
using Godot;

namespace Game.GameContext.VelocityBoosters.Views;

public partial class VelocityBoosterView : Node2D
{
    [Export] public VelocityBoosterAnimationGraphPlayer? AnimationPlayer;
    [Export] public Area2D? Area2D;
    [Export] public float BoostStrenght = 600f;
}