using Game.GameContext.Letters.Views;

namespace Game.GameContext.Letters.UseCases;

public sealed class WhenLetterCollectableCollectedUseCase
{
    readonly ShowLetterUseCase _showLetterUseCase;

    public WhenLetterCollectableCollectedUseCase(
        ShowLetterUseCase showLetterUseCase
        )
    {
        _showLetterUseCase = showLetterUseCase;
    }

    public void Execute(LetterCollectableView letterCollectableView)
    {
        _showLetterUseCase.Execute(letterCollectableView.LetterConfiguration!);
    }
}