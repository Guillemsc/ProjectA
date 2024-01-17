using Game.GameContext.Entities.Services;
using Game.GameContext.Letters.Views;
using Game.GameContext.Saves.Datas;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Letters.UseCases;

public sealed class LoadLettersUseCase
{
    readonly IGameEntitiesService _gameEntitiesService;
    readonly SavesData _savesData;

    public LoadLettersUseCase(
        IGameEntitiesService gameEntitiesService, 
        SavesData savesData
        )
    {
        _gameEntitiesService = gameEntitiesService;
        _savesData = savesData;
    }

    public void Execute()
    {
        _gameEntitiesService.ForEachEntity<LetterCollectableView>(Load);
    }

    void Load(LetterCollectableView letterCollectableView)
    {
        bool collected = _savesData.CurrentMapSaveData.CollectedLettersUids.Contains(letterCollectableView.Uid);

        letterCollectableView.SetNode2DActive(!collected);
    }
}