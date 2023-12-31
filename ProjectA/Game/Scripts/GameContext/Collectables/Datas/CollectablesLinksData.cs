using System;
using System.Collections.Generic;
using Game.GameContext.Collectables.Views;

namespace Game.GameContext.Collectables.Datas;

public sealed class CollectablesLinksData
{
    public Dictionary<Type, Action<CollectableView>> CollectableTypeByAction { get; } = new();
}