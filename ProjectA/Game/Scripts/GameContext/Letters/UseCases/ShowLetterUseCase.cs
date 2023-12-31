using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Letters.Configurations;
using Game.GameContext.LetterUi.Interactors;
using Game.GameContext.Pause.UseCases;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using Game.ServicesContext.Time.Services;
using GUtils.Extensions;
using GUtils.Tasks.Runners;
using GUtils.Time.Extensions;
using GUtilsGodot.Time.Extensions;

namespace Game.GameContext.Letters.UseCases;

public sealed class ShowLetterUseCase
{
    readonly IGameTimesService _gameTimesService;
    readonly IAsyncTaskRunner _asyncTaskRunner;
    readonly ILetterUiInteractor _letterUiInteractor;
    readonly PlayerViewData _playerViewData;
    readonly AwaitLetterContinueInputUseCase _awaitLetterContinueInputUseCase;

    public ShowLetterUseCase(
        IGameTimesService gameTimesService,
        IAsyncTaskRunner asyncTaskRunner,
        ILetterUiInteractor letterUiInteractor, 
        PlayerViewData playerViewData,
        AwaitLetterContinueInputUseCase awaitLetterContinueInputUseCase
        )
    {
        _gameTimesService = gameTimesService;
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

            await _gameTimesService.TimeContext.NewStartedTimer().GodotAwaitReach(0.5f.ToSeconds(), cancellationToken);
            
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