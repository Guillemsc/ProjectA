using Game.GameContext.Trampolines.AnimationGraphPlayers;
using Godot;

namespace Game.GameContext.Trampolines.Views;

public partial class TrampolineView : Node2D
{
    [Export] public TrampolineAnimationGraphPlayer? AnimationPlayer;
    [Export] public float BounceStrenght = 600f;
}