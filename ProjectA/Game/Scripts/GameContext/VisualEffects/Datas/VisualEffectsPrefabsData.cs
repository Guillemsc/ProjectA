using System.Collections.Generic;
using Game.GameContext.VisualEffects.Enums;
using Godot;

namespace Game.GameContext.VisualEffects.Datas;

public sealed class VisualEffectsPrefabsData
{
    public Dictionary<OneShotVisualEffectType, PackedScene> OneShotVisualEffectsPrefabs = new();
}