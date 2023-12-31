using Game.ServicesContext.Time.Services;
using Godot;
using GUtils.Services.Cache;

namespace Game.ServicesContext.Time.Nodes;

public partial class GameTimeAnimationPlayer : AnimationPlayer
{
    readonly ServiceLocatorCachedResult<IGameTimesService> _gameTimesService = new();

    public override void _EnterTree()
    {
        _gameTimesService.Get().TimeContext.OnTimeScaleChanged += WhenTimeScaleChanged;
    }
    
    public override void _ExitTree()
    {
        _gameTimesService.Get().TimeContext.OnTimeScaleChanged -= WhenTimeScaleChanged;
    }

    void WhenTimeScaleChanged()
    {
        SpeedScale = _gameTimesService.Get().TimeContext.TimeScale;
    }
}