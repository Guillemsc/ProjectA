using Game.GameContext.PlayerKillers.Interfaces;
using Game.GameContext.Players.UseCases;

namespace Game.GameContext.PlayerKillers.UseCases;

public sealed class WhenPlayerCollidedWithPlayerKillerUseCase
{
    readonly KillPlayerAndReloadUseCase _killPlayerAndReloadUseCase;

    public WhenPlayerCollidedWithPlayerKillerUseCase(KillPlayerAndReloadUseCase killPlayerAndReloadUseCase)
    {
        _killPlayerAndReloadUseCase = killPlayerAndReloadUseCase;
    }

    public void Execute(IPlayerKillerView playerKillerView)
    {
        _killPlayerAndReloadUseCase.Execute();
    }
}