using Game.GameContext.Players.UseCases;

namespace Game.GameContext.General.UseCases;

public sealed class GameStartUseCase
{
    readonly StartPlayerUseCase _startPlayerUseCase;

    public GameStartUseCase(
        StartPlayerUseCase startPlayerUseCase
        )
    {
        _startPlayerUseCase = startPlayerUseCase;
    }

    public void Execute()
    {
        _startPlayerUseCase.Execute();
    }
}