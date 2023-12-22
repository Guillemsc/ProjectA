using Game.GameContext.Cinematics.UseCases;
using Game.GameContext.Players.UseCases;

namespace Game.GameContext.General.UseCases;

public sealed class GameStartUseCase
{
    readonly StartPlayerUseCase _startPlayerUseCase;
    readonly TryPlayStartingMapCinematicUseCase _tryPlayStartingMapCinematicUseCase;

    public GameStartUseCase(
        StartPlayerUseCase startPlayerUseCase,
        TryPlayStartingMapCinematicUseCase tryPlayStartingMapCinematicUseCase
        )
    {
        _startPlayerUseCase = startPlayerUseCase;
        _tryPlayStartingMapCinematicUseCase = tryPlayStartingMapCinematicUseCase;
    }

    public void Execute()
    {
        _startPlayerUseCase.Execute();
        _tryPlayStartingMapCinematicUseCase.Execute();
    }
}