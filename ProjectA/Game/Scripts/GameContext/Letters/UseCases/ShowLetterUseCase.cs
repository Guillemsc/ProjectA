using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Letters.Configurations;
using Game.GameContext.LetterUi.Interactors;
using GUtils.Tasks.Runners;

namespace Game.GameContext.Letters.UseCases;

public sealed class ShowLetterUseCase
{
    readonly IAsyncTaskRunner _asyncTaskRunner;
    readonly ILetterUiInteractor _letterUiInteractor;
    readonly AwaitLetterContinueInputUseCase _awaitLetterContinueInputUseCase;

    public ShowLetterUseCase(
        IAsyncTaskRunner asyncTaskRunner,
        ILetterUiInteractor letterUiInteractor, 
        AwaitLetterContinueInputUseCase awaitLetterContinueInputUseCase
        )
    {
        _asyncTaskRunner = asyncTaskRunner;
        _letterUiInteractor = letterUiInteractor;
        _awaitLetterContinueInputUseCase = awaitLetterContinueInputUseCase;
    }

    public void Execute(LetterConfiguration configuration)
    {
        async Task Run(CancellationToken cancellationToken)
        {
            _letterUiInteractor.SetText(configuration.Text!);
            await _letterUiInteractor.SetVisible(true, false, cancellationToken);
            await _awaitLetterContinueInputUseCase.Execute(cancellationToken);
            await _letterUiInteractor.SetVisible(false, false, cancellationToken);
        }

        _asyncTaskRunner.Run(Run);
    }
}