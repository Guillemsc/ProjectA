using Game.GameContext.Players.UseCases;

namespace Game.GameContext.General.UseCases;

public sealed class GameStartUseCase
{
    readonly AppearPlayerUseCase _appearPlayerUseCase;

    public GameStartUseCase(
        AppearPlayerUseCase appearPlayerUseCase
        )
    {
        _appearPlayerUseCase = appearPlayerUseCase;
    }

    public void Execute()
    {
        _appearPlayerUseCase.Execute();
    }
}