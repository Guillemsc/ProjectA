using Game.MetaContext.MainMenuUi.Data;
using GUtils.Executables;

namespace Game.MetaContext.MainMenuUi.UseCases;

public sealed class EnableCanUnfoldUseCase : IExecutable
{
    readonly FoldedData _foldedData;

    public EnableCanUnfoldUseCase(FoldedData foldedData)
    {
        _foldedData = foldedData;
    }

    public void Execute()
    {
        _foldedData.CanUnfold = true;
    }
}