using Godot;

namespace Game.GameContext.Collectables.Views;

public partial class CollectableView : RigidBody2D
{
    [Export] public AnimationPlayer? AnimationPlayer;
}