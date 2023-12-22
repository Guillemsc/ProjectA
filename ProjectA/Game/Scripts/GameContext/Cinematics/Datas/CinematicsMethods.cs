using Game.GameContext.Cinematics.UseCases;

namespace Game.GameContext.Cinematics.Datas;

public sealed class CinematicsMethods
{
    public AwaitUntilPlayerIsOnTheGroundUseCase AwaitUntilPlayerIsOnTheGroundUseCase { get; }
    
    public CinematicsMethods(
        AwaitUntilPlayerIsOnTheGroundUseCase awaitUntilPlayerIsOnTheGroundUseCase
        )
    {
        AwaitUntilPlayerIsOnTheGroundUseCase = awaitUntilPlayerIsOnTheGroundUseCase;
    }
}