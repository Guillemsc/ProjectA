using Game.GameContext.Cinematics.UseCases;
using Game.GameContext.General.Configurations;

namespace Game.GameContext.Players.UseCases;

public sealed class CanPlayerPlayAppearAnimationUseCase
{
    readonly GameApplicationContextConfiguration _gameApplicationContextConfiguration;
    readonly HasStartingMapCinematicUseCase _hasStartingMapCinematicUseCase;

    public CanPlayerPlayAppearAnimationUseCase(
        GameApplicationContextConfiguration gameApplicationContextConfiguration, 
        HasStartingMapCinematicUseCase hasStartingMapCinematicUseCase
        )
    {
        _gameApplicationContextConfiguration = gameApplicationContextConfiguration;
        _hasStartingMapCinematicUseCase = hasStartingMapCinematicUseCase;
    }

    public bool Execute()
    {
        bool hasCinematic = _hasStartingMapCinematicUseCase.Execute();
        return _gameApplicationContextConfiguration.PlayerAppears && !hasCinematic;
    }
}