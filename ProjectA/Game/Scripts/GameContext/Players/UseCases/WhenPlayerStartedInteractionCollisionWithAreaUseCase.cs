using System;
using Game.GameContext.Collectables.UseCases;
using Game.GameContext.Collectables.Views;
using Game.GameContext.Connections.UseCases;
using Game.GameContext.Connections.Views;
using Game.GameContext.PlayerKillers.Interfaces;
using Game.GameContext.PlayerKillers.UseCases;
using Game.GameContext.Trampolines.UseCases;
using Game.GameContext.Trampolines.Views;
using Game.GameContext.VelocityBoosters.UseCases;
using Game.GameContext.VelocityBoosters.Views;
using Godot;
using GUtils.Optionals;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Players.UseCases;

public sealed class WhenPlayerStartedInteractionCollisionWithAreaUseCase
{
    readonly WhenPlayerCollidedWithCollectableUseCase _whenPlayerCollidedWithCollectableUseCase;
    readonly WhenPlayerCollidedWithTrampolineUseCase _whenPlayerCollidedWithTrampolineUseCase;
    readonly WhenPlayerCollidedWithVelocityBoosterUseCase _whenPlayerCollidedWithVelocityBoosterUseCase;
    readonly WhenPlayerCollidedWithPlayerKillerUseCase _playerCollidedWithPlayerKillerUseCase;

    public WhenPlayerStartedInteractionCollisionWithAreaUseCase(
        WhenPlayerCollidedWithCollectableUseCase whenPlayerCollidedWithCollectableUseCase, 
        WhenPlayerCollidedWithTrampolineUseCase whenPlayerCollidedWithTrampolineUseCase,
        WhenPlayerCollidedWithVelocityBoosterUseCase whenPlayerCollidedWithVelocityBoosterUseCase,
        WhenPlayerCollidedWithPlayerKillerUseCase playerCollidedWithPlayerKillerUseCase
        )
    {
        _whenPlayerCollidedWithCollectableUseCase = whenPlayerCollidedWithCollectableUseCase;
        _whenPlayerCollidedWithTrampolineUseCase = whenPlayerCollidedWithTrampolineUseCase;
        _whenPlayerCollidedWithVelocityBoosterUseCase = whenPlayerCollidedWithVelocityBoosterUseCase;
        _playerCollidedWithPlayerKillerUseCase = playerCollidedWithPlayerKillerUseCase;
    }

    public void Execute(Area2D area2D)
    {
        if(TryGetAndRun<CollectableView>(area2D, _whenPlayerCollidedWithCollectableUseCase.Execute)) return;
        if(TryGetAndRun<TrampolineView>(area2D, _whenPlayerCollidedWithTrampolineUseCase.Execute)) return;
        if(TryGetAndRun<VelocityBoosterView>(area2D, _whenPlayerCollidedWithVelocityBoosterUseCase.Execute)) return;
        if(TryGetAndRun<IPlayerKillerView>(area2D, _playerCollidedWithPlayerKillerUseCase.Execute)) return;
    }

    bool TryGetAndRun<T>(Area2D area2D, Action<T> action) 
    {
        Optional<T> optionalNode = area2D.GetNodeOnParentHierarchy<T>();

        bool hasCollectableView = optionalNode.TryGet(out T node);

        if (!hasCollectableView)
        {
            return false;
        }
        
        action.Invoke(node);

        return true;
    }
}