using GUtils.Time.TimeContexts;

namespace Game.ServicesContext.Time.Services;

public interface IGameTimesService
{
    ITimeContext TimeContext { get; }
    ITimeContext PhysicsTimeContext { get; }
}