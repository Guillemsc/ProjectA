using GTweens.Tweens;
using GUtils.Time.TimeContexts;

namespace Game.ServicesContext.Time.Tweens.Extensions;

public static class TimeTweensExtensions
{
    public static void LinkTweenToGameTime(this GTween tween, ITimeContext timeContext)
    {
        void Register()
            => timeContext.OnTimeScaleChanged += WhenTimeScaleChanges;
        
        void Unregister()
            => timeContext.OnTimeScaleChanged -= WhenTimeScaleChanges;
        
        void WhenTimeScaleChanges()
            => tween.SetTimeScale(timeContext.TimeScale);

        tween.OnStartAction += Register;
        tween.OnKillAction += Unregister;
    }
}