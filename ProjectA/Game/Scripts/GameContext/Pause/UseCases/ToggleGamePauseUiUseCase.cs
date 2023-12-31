using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Pause.Datas;
using Game.GameContext.PauseUi.Interactors;
using GUtils.Tasks.Runners;

namespace Game.GameContext.Pause.UseCases;

public sealed class ToggleGamePauseUiUseCase
{
    readonly PauseData _pauseData;
    readonly IAsyncTaskRunner _asyncTaskRunner;
    readonly IPauseUiInteractor _pauseUiInteractor;
    readonly SetGameLogicPausedUseCase _setGameLogicPausedUseCase;

    public ToggleGamePauseUiUseCase(
        PauseData pauseData,
        IAsyncTaskRunner asyncTaskRunner,
        IPauseUiInteractor pauseUiInteractor, 
        SetGameLogicPausedUseCase setGameLogicPausedUseCase
        )
    {
        _pauseData = pauseData;
        _asyncTaskRunner = asyncTaskRunner;
        _pauseUiInteractor = pauseUiInteractor;
        _setGameLogicPausedUseCase = setGameLogicPausedUseCase;
    }

    public void Execute()
    {
        _pauseData.Paused = !_pauseData.Paused;

        async Task Run(CancellationToken cancellationToken)
        {
            if (_pauseData.Paused)
            {
                _setGameLogicPausedUseCase.Execute(true, this);
            }
            
            await _pauseUiInteractor.SetVisible(_pauseData.Paused, false, cancellationToken);
            
            if (!_pauseData.Paused)
            {
                _setGameLogicPausedUseCase.Execute(false, this);
            }
        }

        _asyncTaskRunner.Run(Run);
    }
}