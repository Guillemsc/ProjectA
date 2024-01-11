using System.Collections.Generic;
using Game.GameContext.Players.AnimationGraphPlayers;
using Godot;
using Godot.Collections;
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
   
   [Export] public Array<RayCast2D> LeftSqushedRayCasts;
   [Export] public Array<RayCast2D> RightSqushedRayCasts;
   [Export] public Array<RayCast2D> TopSqushedRayCasts;
   [Export] public Array<RayCast2D> BottomSqushedRayCasts;

   public Vector2 MovementVelocity;
   
   public bool CanUpdateMovement;
   public bool CanMove;
   public bool CanJump;
   public bool CanDoubleJump;
   
   public Vector2 UncontrolledSpeed = Vector2.Zero;

   public readonly List<Vector2> PreviousPositions = new();
}