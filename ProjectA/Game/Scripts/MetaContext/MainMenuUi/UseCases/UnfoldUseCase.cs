using System.Threading;
using System.Threading.Tasks;
using Game.MetaContext.MainMenuUi.Data;
using Godot;
using GUtils.Tasks.Runners;
using GUtilsGodot.Extensions;

namespace Game.MetaContext.MainMenuUi.UseCases;

public sealed class UnfoldUseCase
{
    readonly AnimationPlayer _foldingAnimationPlayer;
    readonly FoldedData _foldedData;
    readonly IAsyncTaskRunner _asyncTaskRunner;
    readonly SetInitialFocusUseCase _setInitialFocusUseCase;

    public UnfoldUseCase(
        AnimationPlayer foldingAnimationPlayer, 
        FoldedData foldedData, 
        IAsyncTaskRunner asyncTaskRunner, 
        SetInitialFocusUseCase setInitialFocusUseCase
        )
    {
        _foldingAnimationPlayer = foldingAnimationPlayer;
        _foldedData = foldedData;
        _asyncTaskRunner = asyncTaskRunner;
        _setInitialFocusUseCase = setInitialFocusUseCase;
    }

    public void Execute()
    {
        if (!_foldedData.Folded)
        {
            return;
        }

        _foldedData.Folded = false;
        
        async Task Run(CancellationToken cancellationToken)
        {
            _setInitialFocusUseCase.Execute();
            
            await _foldingAnimationPlayer.PlayAndAwaitCompletition("Unfold", cancellationToken);
        }

        _asyncTaskRunner.Run(Run);
    }
}