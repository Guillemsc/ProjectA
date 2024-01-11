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
    [Export] public Area2D? WallDetectorArea2d;
    [Export] public Array<CardinalDirection>? MovementSequence;
    [Export] public AngryBlockMovementConfiguration? MovementConfiguration;
    
    public TimeSpan TimeLeftUntilNextBlink = TimeSpan.Zero;
    public int CurrentMovementSequenceIndex;
    public float CurrentMovementSpeed;
}