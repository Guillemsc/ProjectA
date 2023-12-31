using Game.GameContext.Pause.Datas;
using Game.ServicesContext.Time.Services;

namespace Game.GameContext.Pause.UseCases;

public sealed class SetGameLogicPausedUseCase
{
    readonly IGameTimesService _gameTimesService;
    readonly PauseData _pauseData;

    public SetGameLogicPausedUseCase(
        IGameTimesService gameTimesService, 
        PauseData pauseData
        )
    {
        _gameTimesService = gameTimesService;
        _pauseData = pauseData;
    }

    public void Execute(bool paused, object owner)
    {
        _pauseData.LogicPaused.SetActive(owner, paused);
        
        float timeScale = _pauseData.LogicPaused.IsActive() ? 0f : 1f;

        _gameTimesService.TimeContext.TimeScale = timeScale;
        _gameTimesService.PhysicsTimeContext.TimeScale = timeScale;
    }
}