using Game.GameContext.Collectables.Enums;
using Game.GameContext.Collectables.Views;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Collectables.UseCases;

public sealed class CollectCollectableUseCase
{
    public void Execute(CollectableView collectableView)
    {
        collectableView.AnimationPlayer!.Play(CollectableAnimationState.Collected);
    }
}