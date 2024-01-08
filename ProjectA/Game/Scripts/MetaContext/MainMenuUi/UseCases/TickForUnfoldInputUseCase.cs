using Game.MetaContext.MainMenuUi.Data;
using Godot;
using GUtils.Executables;

namespace Game.MetaContext.MainMenuUi.UseCases;

public sealed class TickForUnfoldInputUseCase : IExecutable
{
    readonly FoldedData _foldedData;
    readonly UnfoldUseCase _unfoldUseCase;

    public TickForUnfoldInputUseCase(
        FoldedData foldedData,
        UnfoldUseCase unfoldUseCase
        )
    {
        _foldedData = foldedData;
        _unfoldUseCase = unfoldUseCase;
    }

    public void Execute()
    {
        if (!_foldedData.CanUnfold)
        {
            return;
        }
        
        bool cancel = Input.IsActionJustPressed("ui_accept");

        if (cancel)
        {
            _unfoldUseCase.Execute();
        }
    }
}