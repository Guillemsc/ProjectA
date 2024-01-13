using System;
using Game.GameContext.AngryBlocks.AnimationGraphPlayers;
using Game.GameContext.AngryBlocks.Configurations;
using Godot;
using Godot.Collections;
using GUtils.Directions;

namespace Game.GameContext.AngryBlocks.Views;

public partial class AngryBlockView : AnimatableBody2D
{
    [Export] public AngryBlockAnimationGraphPlayerNode? AnimationGraphPlayer;
    [Export] public Area2D? LeftWallDetectorArea2d;
    [Export] public Area2D? RightWallDetectorArea2d;
    [Export] public Area2D? TopWallDetectorArea2d;
    [Export] public Area2D? BottomWallDetectorArea2d;
    [Export] public Area2D? LeftPlayerKillerArea2d;
    [Export] public Area2D? RightPlayerKillerArea2d;
    [Export] public Area2D? TopPlayerKillerArea2d;
    [Export] public Area2D? BottomPlayerKillerArea2d;
    [Export] public Array<CardinalDirection>? MovementSequence;
    [Export] public AngryBlockMovementConfiguration? MovementConfiguration;
    
    public TimeSpan TimeLeftUntilNextBlink = TimeSpan.Zero;
    public int CurrentMovementSequenceIndex;
    public float CurrentMovementSpeed;
}