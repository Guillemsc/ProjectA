using Godot;

namespace Game.GameContext.AngryBlocks.Configurations;

[GlobalClass]
public partial class GameAngryBlocksConfiguration : Resource
{
    [Export] public float MaxBlinkTimeSeconds = 5;
    [Export] public float MinBlinkTimeSeconds = 2;
}