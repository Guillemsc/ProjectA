using Game.GameContext.Players.Enums;
using Godot;
using GUtils.Directions;
using GUtils.Locations.Enums;

namespace Game.GameContext.Players.Views;

public partial class PlayerView : CharacterBody2D
{
   [Export] public AnimatedSprite2D? AnimatedSprite;
   [Export] public AnimationGraphPlayers.PlayerAnimationGraphPlayerNode? AnimationPlayer;
   [Export] public Area2D? LeftWallDetector;
   [Export] public Area2D? RightWallDetector;
   [Export] public Area2D? InteractionsDetector;

   public bool CanMove;
   public bool CanJump;
   public bool CanDoubleJump;
   
   public Vector2 UncontrolledSpeed = Vector2.Zero;
}