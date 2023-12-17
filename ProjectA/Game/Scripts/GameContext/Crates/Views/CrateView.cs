using Game.GameContext.Crates.AnimationGraphPlayers;
using Godot;

namespace Game.GameContext.Crates.Views;

public partial class CrateView : Node2D
{
    [Export] public CrateAnimationGraphPlayer? AnimationPlayer;
    [Export] public float BounceStrenght = 300f;
    [Export] public int HitsToDestroy = 1;
    [Export] public int FruitsPerHit = 0;
    [Export] public int FruitsWhenDestroyed = 3;

    public int HitsCount;
}