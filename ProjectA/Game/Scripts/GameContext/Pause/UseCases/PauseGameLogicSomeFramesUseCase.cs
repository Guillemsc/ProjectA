using System;
using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Pause.Configurations;
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

    public void Execute()
    {
        TimeSpan pauseDuration = _gamePauseConfiguration!.PauseGameLogicSomeFramesDurationSeconds.ToSeconds();

        async Task Run(CancellationToken cancellationToken)
        {
            _setGameLogicPausedUseCase!.Execute(true, this);
            
            ITimer timer = StopwatchTimer.FromStarted();

            while (timer.Time < pauseDuration)
            {
                await TaskExtensions.GodotYield();
            }
            
            _setGameLogicPausedUseCase.Execute(false, this);
        }

        _asyncTaskRunner!.Run(Run);
    }
}