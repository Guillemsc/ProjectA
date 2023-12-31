using GUtils.Time.TimeContexts;

namespace Game.ServicesContext.Time.Services;

public sealed class GameTimesService : IGameTimesService
{
    public ITimeContext TimeContext { get; }
    public ITimeContext PhysicsTimeContext { get; }
    
    public GameTimesService(ITimeContext timeContext, ITimeContext physicsTimeContext)
    {
        TimeContext = timeContext;
        PhysicsTimeContext = physicsTimeContext;
    }
}