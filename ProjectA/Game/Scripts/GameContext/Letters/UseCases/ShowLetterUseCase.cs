using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Letters.Configurations;
using Game.GameContext.LetterUi.Interactors;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using GUtils.Tasks.Runners;

namespace Game.GameContext.Letters.UseCases;

public sealed class ShowLetterUseCase
{
    readonly IAsyncTaskRunner _asyncTaskRunner;
    readonly ILetterUiInteractor _letterUiInteractor;
    readonly PlayerViewData _playerViewData;
    readonly AwaitLetterContinueInputUseCase _awaitLetterContinueInputUseCase;

    public ShowLetterUseCase(
        IAsyncTaskRunner asyncTaskRunner,
        ILetterUiInteractor letterUiInteractor, 
        PlayerViewData playerViewData,
        AwaitLetterContinueInputUseCase awaitLetterContinueInputUseCase
        )
    {
        _asyncTaskRunner = asyncTaskRunner;
        _letterUiInteractor = letterUiInteractor;
        _playerViewData = playerViewData;
        _awaitLetterContinueInputUseCase = awaitLetterContinueInputUseCase;

    }

    public void Execute(LetterConfiguration configuration)
    {
        async Task Run(CancellationToken cancellationToken)
        {
            bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);
        
            if(!hasPlayer)
            {
                return;
            }

            playerView.CanMove = false;
            
            _letterUiInteractor.SetText(configuration.Text!);
            await _letterUiInteractor.SetVisible(true, false, cancellationToken);
            
            if(cancellationToken.IsCancellationRequested) return;
            
            await _awaitLetterContinueInputUseCase.Execute(cancellationToken);
            
            if(cancellationToken.IsCancellationRequested) return;
            
            await _letterUiInteractor.SetVisible(false, false, cancellationToken);
            
            playerView.CanMove = true;
        }

        _asyncTaskRunner.Run(Run);
    }
}