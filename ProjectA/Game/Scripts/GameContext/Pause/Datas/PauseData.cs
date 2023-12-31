using GUtils.ActiveSources;

namespace Game.GameContext.Pause.Datas;

public sealed class PauseData
{
    public bool Paused;
    public ISingleActiveSource LogicPaused { get; } = new SingleActiveSource();
}