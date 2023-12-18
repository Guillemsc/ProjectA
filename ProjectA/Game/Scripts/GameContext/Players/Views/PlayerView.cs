using System.Collections.Generic;
using Game.GameContext.Players.AnimationGraphPlayers;
using Godot;
using GUtilsGodot.CharacterBody2Ds.Callbacks;

namespace Game.GameContext.Players.Views;

public partial class PlayerView : CharacterBody2D
{
   [Export] public CharacterBody2DCollisionCallbacks? CharacterBody2DCollisionCallbacks;
   [Export] public AnimatedSprite2D? AnimatedSprite;
   [Export] public PlayerAnimationGraphPlayerNode? AnimationPlayer;
   [Export] public Area2D? LeftWallDetector;
   [Export] public Area2D? RightWallDetector;
   [Export] public Area2D? InteractionsDetector;

   public bool CanUpdateMovement;
   public bool CanMove;
   public bool CanJump;
   public bool CanDoubleJump;
   
   public Vector2 UncontrolledSpeed = Vector2.Zero;

   public List<Vector2> PreviousPositions = new();
}