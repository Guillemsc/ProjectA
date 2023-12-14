using Game.GameContext.Player.Enums;
using Godot;
using GUtils.Directions;
using GUtils.Locations.Enums;

namespace Game.GameContext.Player.Views;

public partial class PlayerView : CharacterBody2D
{
   [Export] public AnimatedSprite2D? AnimatedSprite;
   [Export] public Area2D? LeftWallDetector;
   [Export] public Area2D? RightWallDetector;
   
   public bool OnAir;
   public PlayerOnAirState OnAirState;
   
   public bool OnWall;
   public HorizontalLocation OnWallLocation;
   
   public bool MovingHorizontally;
   public HorizontalDirection HorizontalDirection;
}