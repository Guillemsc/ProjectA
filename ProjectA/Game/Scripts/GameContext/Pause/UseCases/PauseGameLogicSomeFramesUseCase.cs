using System;
using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Pause.Configurations;
using Game.GameContext.Pause.Enums;
using GUtils.Extensions;
using GUtils.Pooling.Objects;
using GUtils.Tasks.Runners;
using GUtils.Time.Timers;
using TaskExtensions = GUtilsGodot.Extensions.TaskExtensions;

namespace Game.GameContext.Pause.UseCases;

public sealed class PauseGameLogicSomeFramesUseCase : IReturnablePooledObject
{
    GamePauseConfiguration? _gamePauseConfiguration;
    IAsyncTaskRunner? _asyncTaskRunner;
    SetGameLogicPausedUseCase? _setGameLogicPausedUseCase;

    public void Init(
        GamePauseConfiguration gamePauseConfiguration,
        IAsyncTaskRunner asyncTaskRunner, 
        SetGameLogicPausedUseCase setGameLogicPausedUseCase
        )
    {
        _gamePauseConfiguration = gamePauseConfiguration;
        _asyncTaskRunner = asyncTaskRunner;
        _setGameLogicPausedUseCase = setGameLogicPausedUseCase;
    }
    
    public void PooledObjectReturned()
    {
        _gamePauseConfiguration = null;
        _asyncTaskRunner = null;
        _setGameLogicPausedUseCase = null;
    }

    public void Execute(PauseFramesDuration pauseFramesDuration)
    {
        float seconds = pauseFramesDuration switch
        {
            PauseFramesDuration.Short => _gamePauseConfiguration!.PauseGameLogicSomeFramesShortDurationSeconds,
            PauseFramesDuration.Medium => _gamePauseConfiguration!.PauseGameLogicSomeFramesMediumDurationSeconds,
            PauseFramesDuration.Long => _gamePauseConfiguration!.PauseGameLogicSomeFramesLongDurationSeconds,
            PauseFramesDuration.VeryLong => _gamePauseConfiguration!.PauseGameLogicSomeFramesVeryLongDurationSeconds,
            _ => _gamePauseConfiguration!.PauseGameLogicSomeFramesShortDurationSeconds
        };
        
        TimeSpan pauseDuration = seconds.ToSeconds();

        async Task Run(CancellationToken cancellationToken)
        {
            _setGameLogicPausedUseCase!.Execute(true, this);
            
            ITimer timer = StopwatchTimer.FromStarted();

            while (!cancellationToken.IsCancellationRequested && timer.Time < pauseDuration)
            {
                await TaskExtensions.GodotYield();
            }
            
            if(cancellationToken.IsCancellationRequested) return;
            
            _setGameLogicPausedUseCase.Execute(false, this);
        }

        _asyncTaskRunner!.Run(Run);
    }
}