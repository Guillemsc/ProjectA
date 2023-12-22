using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Collectables.Enums;
using Game.GameContext.Collectables.Views;
using Game.GameContext.VisualEffects.Enums;
using Game.GameContext.VisualEffects.UseCases;
using GUtils.Tasks.Runners;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Collectables.UseCases;

public sealed class WhenPlayerCollidedWithCollectableUseCase
{
    readonly IAsyncTaskRunner _asyncTaskRunner;
    readonly PlayOneShotVisualEffectUseCase _playOneShotVisualEffectUseCase;

    public WhenPlayerCollidedWithCollectableUseCase(
        IAsyncTaskRunner asyncTaskRunner,
        PlayOneShotVisualEffectUseCase playOneShotVisualEffectUseCase
        )
    {
        _asyncTaskRunner = asyncTaskRunner;
        _playOneShotVisualEffectUseCase = playOneShotVisualEffectUseCase;
    }

    public void Execute(CollectableView collectableView)
    {
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