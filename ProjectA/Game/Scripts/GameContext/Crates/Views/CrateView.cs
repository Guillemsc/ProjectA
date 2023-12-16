using System;
using Game.GameContext.Crates.AnimationGraphPlayers;
using Godot;

namespace Game.GameContext.Crates.Views;

public partial class CrateView : Node2D
{
    [Export] public CrateAnimationGraphPlayer? AnimationPlayer;
    [Export] public float BounceStrenght = 300f;
}