using System;
using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Collectables.Datas;
using Game.GameContext.Collectables.Enums;
using Game.GameContext.Collectables.Views;
using Game.GameContext.Letters.UseCases;
using Game.GameContext.Letters.Views;
using Game.GameContext.VisualEffects.Enums;
using Game.GameContext.VisualEffects.UseCases;
using GUtils.Tasks.Runners;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Collectables.UseCases;

public sealed class WhenPlayerCollidedWithCollectableUseCase
{
    readonly IAsyncTaskRunner _asyncTaskRunner;
    readonly CollectablesLinksData _collectablesLinksData;
    readonly PlayOneShotVisualEffectUseCase _playOneShotVisualEffectUseCase;

    public WhenPlayerCollidedWithCollectableUseCase(
        IAsyncTaskRunner asyncTaskRunner,
        CollectablesLinksData collectablesLinksData,
        PlayOneShotVisualEffectUseCase playOneShotVisualEffectUseCase
        )
    {
        _asyncTaskRunner = asyncTaskRunner;
        _collectablesLinksData = collectablesLinksData;
        _playOneShotVisualEffectUseCase = playOneShotVisualEffectUseCase;
    }

    public void Execute(CollectableView collectableView)
    {
        Type actualType = collectableView.GetType();

        bool typeFound = _collectablesLinksData.CollectableTypeByAction.TryGetValue(
            actualType,
            out Action<CollectableView>? action
        );

        if (typeFound)
        {
            action?.Invoke(collectableView);
        }
        
        async Task Run(CancellationToken cancellationToken)
        {
            collectableView.AnimationPlayer!.Play(CollectableAnimationState.Collected);
            await collectableView.AnimationPlayer!.AwaitCompletition(cancellationToken);
            
            if(cancellationToken.IsCancellationRequested) return;

            _playOneShotVisualEffectUseCase.Execute(
                OneShotVisualEffectType.CollectableCollected,
                collectableView.GlobalPosition
            );
        }

        _asyncTaskRunner.Run(Run);
    }
}