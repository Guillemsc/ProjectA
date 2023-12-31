using System.Threading;

namespace Game.GameContext.Cinematics.Datas;

public sealed class CurrentCinematicData
{
    public CancellationTokenSource? CurrentCinematicSkipTokenSource;
}