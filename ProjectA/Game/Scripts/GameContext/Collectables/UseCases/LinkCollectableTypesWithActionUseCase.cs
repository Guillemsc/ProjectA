using Game.GameContext.Collectables.Datas;
using Game.GameContext.Letters.UseCases;
using Game.GameContext.Letters.Views;

namespace Game.GameContext.Collectables.UseCases;

public sealed class LinkCollectableTypesWithActionUseCase
{
    readonly CollectablesLinksData _collectablesLinksData;
    readonly WhenLetterCollectableCollectedUseCase _whenLetterCollectableCollectedUseCase;

    public LinkCollectableTypesWithActionUseCase(
        CollectablesLinksData collectablesLinksData, 
        WhenLetterCollectableCollectedUseCase whenLetterCollectableCollectedUseCase
        )
    {
        _collectablesLinksData = collectablesLinksData;
        _whenLetterCollectableCollectedUseCase = whenLetterCollectableCollectedUseCase;
    }

    public void Execute()
    {
        _collectablesLinksData.CollectableTypeByAction.Add(
            typeof(LetterCollectableView),
            o => _whenLetterCollectableCollectedUseCase.Execute((LetterCollectableView)o)
        );
    }
}