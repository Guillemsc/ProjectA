using System;
using Game.GameContext.Cinematics.UseCases;
using Game.GameContext.Cinematics.Views;
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
    readonly WhenPlayerCollidedWithPlayerKillerUseCase _whenPlayerCollidedWithPlayerKillerUseCase;
    readonly WhenPlayerCollidedWithCinematicTriggerUseCase _whenPlayerCollidedWithCinematicTriggerUseCase;

    public WhenPlayerStartedInteractionCollisionWithAreaUseCase(
        WhenPlayerCollidedWithCollectableUseCase whenPlayerCollidedWithCollectableUseCase, 
        WhenPlayerCollidedWithTrampolineUseCase whenPlayerCollidedWithTrampolineUseCase,
        WhenPlayerCollidedWithVelocityBoosterUseCase whenPlayerCollidedWithVelocityBoosterUseCase,
        WhenPlayerCollidedWithPlayerKillerUseCase whenPlayerCollidedWithPlayerKillerUseCase,
        WhenPlayerCollidedWithCinematicTriggerUseCase whenPlayerCollidedWithCinematicTriggerUseCase
        )
    {
        _whenPlayerCollidedWithCollectableUseCase = whenPlayerCollidedWithCollectableUseCase;
        _whenPlayerCollidedWithTrampolineUseCase = whenPlayerCollidedWithTrampolineUseCase;
        _whenPlayerCollidedWithVelocityBoosterUseCase = whenPlayerCollidedWithVelocityBoosterUseCase;
        _whenPlayerCollidedWithPlayerKillerUseCase = whenPlayerCollidedWithPlayerKillerUseCase;
        _whenPlayerCollidedWithCinematicTriggerUseCase = whenPlayerCollidedWithCinematicTriggerUseCase;
    }

    public void Execute(Area2D area2D)
    {
        if(TryGetAndRun<CollectableView>(area2D, _whenPlayerCollidedWithCollectableUseCase.Execute)) return;
        if(TryGetAndRun<TrampolineView>(area2D, _whenPlayerCollidedWithTrampolineUseCase.Execute)) return;
        if(TryGetAndRun<VelocityBoosterView>(area2D, _whenPlayerCollidedWithVelocityBoosterUseCase.Execute)) return;
        if(TryGetAndRun<IPlayerKillerView>(area2D, _whenPlayerCollidedWithPlayerKillerUseCase.Execute)) return;
        if(TryGetAndRun<CinematicTriggerView>(area2D, _whenPlayerCollidedWithCinematicTriggerUseCase.Execute)) return;
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