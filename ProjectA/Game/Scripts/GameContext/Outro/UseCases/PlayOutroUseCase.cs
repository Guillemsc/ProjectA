using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.OutroUi.Interactors;

namespace Game.GameContext.Outro.UseCases;

public sealed class PlayOutroUseCase
{
    readonly IOutroUiInteractor _outroUiInteractor;
    readonly GoToMainMenuAfterOutroUseCase _goToMainMenuAfterOutroUseCase;

    public PlayOutroUseCase(
        IOutroUiInteractor outroUiInteractor, 
        GoToMainMenuAfterOutroUseCase goToMainMenuAfterOutroUseCase
        )
    {
        _outroUiInteractor = outroUiInteractor;
        _goToMainMenuAfterOutroUseCase = goToMainMenuAfterOutroUseCase;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        await _outroUiInteractor.SetVisible(true, false, cancellationToken);
        await _outroUiInteractor.SetVisible(false, false, cancellationToken);
        
        _goToMainMenuAfterOutroUseCase.Execute();
    }
}