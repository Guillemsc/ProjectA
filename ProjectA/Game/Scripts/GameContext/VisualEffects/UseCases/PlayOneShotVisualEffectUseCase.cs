using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.General.Datas;
using Game.GameContext.VisualEffects.Datas;
using Game.GameContext.VisualEffects.Enums;
using Game.GameContext.VisualEffects.Views;
using Godot;
using GUtils.Tasks.Runners;
using GUtilsGodot.Extensions;

namespace Game.GameContext.VisualEffects.UseCases;

public sealed class PlayOneShotVisualEffectUseCase
{
    readonly GameGeneralViewData _gameGeneralViewData;
    readonly VisualEffectsPrefabsData _visualEffectsPrefabsData;
    readonly IAsyncTaskRunner _asyncTaskRunner;

    public PlayOneShotVisualEffectUseCase(
        GameGeneralViewData gameGeneralViewData,
        VisualEffectsPrefabsData visualEffectsPrefabsData,
        IAsyncTaskRunner asyncTaskRunner
        )
    {
        _gameGeneralViewData = gameGeneralViewData;
        _visualEffectsPrefabsData = visualEffectsPrefabsData;
        _asyncTaskRunner = asyncTaskRunner;
    }

    public void Execute(OneShotVisualEffectType oneShotVisualEffectType, Vector2 globalPosition)
    {
        bool prefabFound = _visualEffectsPrefabsData.OneShotVisualEffectsPrefabs.TryGetValue(
            oneShotVisualEffectType,
            out PackedScene? packedScene
        );

        if (!prefabFound)
        {
            return;
        }

        OneShotVisualEffectView effectView = packedScene!.Instantiate<OneShotVisualEffectView>();

        async Task Play(CancellationToken cancellationToken)
        {
            effectView.SetParent(_gameGeneralViewData.Root);
            effectView.GlobalPosition = globalPosition;

            await effectView.Play(cancellationToken);
            
            effectView.QueueFree();
        }

        _asyncTaskRunner.Run(Play);
    }
}