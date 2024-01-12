using Game.GameContext.Pause.UseCases;
using GUtils.Pooling.Pools;

namespace Game.GameContext.Pools;

public static class GamePools
{
    public static readonly ObjectPool<PauseGameLogicSomeFramesUseCase> PauseGameLogicSomeFramesUseCasePool = new(() => new PauseGameLogicSomeFramesUseCase());
}