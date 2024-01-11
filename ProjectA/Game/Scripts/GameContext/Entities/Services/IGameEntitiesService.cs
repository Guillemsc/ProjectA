using System;
using Godot;

namespace Game.GameContext.Entities.Services;

public interface IGameEntitiesService
{
    void Register<T>(T entity) where T : Node2D;
    void ForEachEntity<T>(Action<T> action) where T : Node2D;
}