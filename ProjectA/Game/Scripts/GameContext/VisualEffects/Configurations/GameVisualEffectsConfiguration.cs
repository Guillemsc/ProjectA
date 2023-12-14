using Godot;

namespace Game.GameContext.VisualEffects.Configurations;

[GlobalClass]
public partial class GameVisualEffectsConfiguration : Resource
{
    [Export] public PackedScene? CollectableCollectedVisualEffectPrefab;
}