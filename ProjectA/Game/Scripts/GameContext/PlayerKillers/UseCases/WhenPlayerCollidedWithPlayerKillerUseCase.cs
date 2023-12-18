using Game.GameContext.PlayerKillers.Interfaces;
using Game.GameContext.Players.UseCases;

namespace Game.GameContext.PlayerKillers.UseCases;

public sealed class WhenPlayerCollidedWithPlayerKillerUseCase
{
    readonly KillPlayerUseCase _killPlayerUseCase;

    public WhenPlayerCollidedWithPlayerKillerUseCase(KillPlayerUseCase killPlayerUseCase)
    {
        _killPlayerUseCase = killPlayerUseCase;
    }

    public void Execute(IPlayerKillerView playerKillerView)
    {
        _killPlayerUseCase.Execute();
    }
}