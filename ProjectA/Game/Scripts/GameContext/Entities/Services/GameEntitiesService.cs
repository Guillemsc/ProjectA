using System;
using System.Collections.Generic;
using Godot;

namespace Game.GameContext.Entities.Services;

public sealed class GameEntitiesService : IGameEntitiesService
{
    readonly Dictionary<Type, List<Node2D>> _entities = new();

    public void Register<T>(T entity) where T : Node2D
    {
        Type type = entity.GetType();

        bool hasList = _entities.TryGetValue(type, out List<Node2D>? list);

        if (!hasList)
        {
            list = new List<Node2D>();
            _entities.Add(type, list);
        }
        
        list!.Add(entity);
    }

    public void ForEachEntity<T>(Action<T> action) where T : Node2D
    {
        Type type = typeof(T);
        
        bool hasList = _entities.TryGetValue(type, out List<Node2D>? list);

        if (!hasList)
        {
            return;
        }

        foreach (Node2D node in list!)
        {
            action.Invoke((T)node);
        }
    }
}