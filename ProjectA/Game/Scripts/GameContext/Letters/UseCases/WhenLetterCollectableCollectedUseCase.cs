using Game.GameContext.Letters.Views;
using Game.GameContext.Saves.Datas;

namespace Game.GameContext.Letters.UseCases;

public sealed class WhenLetterCollectableCollectedUseCase
{
    readonly SavesData _savesData;
    readonly ShowLetterUseCase _showLetterUseCase;

    public WhenLetterCollectableCollectedUseCase(
        SavesData savesData,
        ShowLetterUseCase showLetterUseCase
        )
    {
        _savesData = savesData;
        _showLetterUseCase = showLetterUseCase;
    }

    public void Execute(LetterCollectableView letterCollectableView)
    {
        _savesData.CurrentMapSaveData.CollectedLettersUids.Add(letterCollectableView.Uid);
        _showLetterUseCase.Execute(letterCollectableView.LetterConfiguration!);
    }
}