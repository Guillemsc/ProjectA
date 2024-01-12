using Game.GameContext.PlayerKillers.Interfaces;
using Game.GameContext.Saws.AnimationGraphPlayers;
using Game.GameContext.Saws.Configurations;
using Godot;

namespace GUtils.Saws.Views;

public partial class SawView : PathFollow2D, IPlayerKillerView
{
    [Export] public SawAnimationGraphPlayerNode? AnimationGraphPlayer;
    [Export] public SawMovementConfiguration? MovementConfiguration;

    public float CurrentDirection = 1f;
}