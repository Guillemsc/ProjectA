using Game.GameContext.Collectables.UseCases;
using Game.GameContext.Collectables.Views;
using Godot;
using GUtils.Optionals;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Players.UseCases;

public sealed class WhenPlayerStartedCollisionWithInteractionUseCase
{
    readonly CollectCollectableUseCase _collectCollectableUseCase;

    public WhenPlayerStartedCollisionWithInteractionUseCase(
        CollectCollectableUseCase collectCollectableUseCase
        )
    {
        _collectCollectableUseCase = collectCollectableUseCase;
    }

    public void Execute(Area2D area2D)
    {
        Optional<CollectableView> optionalCollectableView = area2D.GetNodeOnParentHierarchy<CollectableView>();

        bool hasCollectableView = optionalCollectableView.TryGet(out CollectableView collectableView);

        if (!hasCollectableView)
        {
            return;
        }
        
        _collectCollectableUseCase.Execute(collectableView);
    }
}