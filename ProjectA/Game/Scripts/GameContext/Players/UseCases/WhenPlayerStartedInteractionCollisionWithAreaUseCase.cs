using Game.GameContext.Collectables.UseCases;
using Game.GameContext.Collectables.Views;
using Game.GameContext.Crates.Views;
using Godot;
using GUtils.Optionals;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Players.UseCases;

public sealed class WhenPlayerStartedInteractionCollisionWithAreaUseCase
{
    readonly CollectCollectableUseCase _collectCollectableUseCase;

    public WhenPlayerStartedInteractionCollisionWithAreaUseCase(
        CollectCollectableUseCase collectCollectableUseCase
        )
    {
        _collectCollectableUseCase = collectCollectableUseCase;
    }

    public void Execute(Area2D area2D)
    {
        Optional<CollectableView> optionalCollectableView = area2D.GetNodeOnParentHierarchy<CollectableView>();

        bool hasCollectableView = optionalCollectableView.TryGet(out CollectableView collectableView);

        if (hasCollectableView)
        {
            _collectCollectableUseCase.Execute(collectableView);
        }
        
        Optional<CrateView> optionalCrateView = area2D.GetNodeOnParentHierarchy<CrateView>();

        bool hasCrateView = optionalCrateView.TryGet(out CrateView crateView);

        if (hasCrateView)
        {
            GD.Print("Crate!");
        }
    }
}