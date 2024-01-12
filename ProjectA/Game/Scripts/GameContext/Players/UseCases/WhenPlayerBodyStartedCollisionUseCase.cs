using Game.GameContext.Crates.UseCases;
using Game.GameContext.Crates.Views;
using Godot;
using GUtils.Optionals;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Players.UseCases;

public sealed class WhenPlayerBodyStartedCollisionUseCase
{
    readonly WhenPlayerCollidedWithCrateUseCase _whenPlayerCollidedWithCrateUseCase;

    public WhenPlayerBodyStartedCollisionUseCase(
        WhenPlayerCollidedWithCrateUseCase whenPlayerCollidedWithCrateUseCase
        )
    {
        _whenPlayerCollidedWithCrateUseCase = whenPlayerCollidedWithCrateUseCase;
    }

    public void Execute(Node2D node, KinematicCollision2D kinematicCollision2D)
    {
        Optional<CrateView> optionalCrateView = node.GetNodeOnParentHierarchy<CrateView>();

        bool hasCrateView = optionalCrateView.TryGet(out CrateView crateView);

        if (hasCrateView)
        {
            _whenPlayerCollidedWithCrateUseCase.Execute(crateView, kinematicCollision2D);
        }
    }
}