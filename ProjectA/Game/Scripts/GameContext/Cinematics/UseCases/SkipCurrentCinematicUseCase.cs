using Game.GameContext.Cinematics.Datas;

namespace Game.GameContext.Cinematics.UseCases;

public sealed class SkipCurrentCinematicUseCase
{
    readonly CurrentCinematicData _currentCinematicData;

    public SkipCurrentCinematicUseCase(
        CurrentCinematicData currentCinematicData
        )
    {
        _currentCinematicData = currentCinematicData;
    }

    public void Execute()
    {
        _currentCinematicData.CurrentCinematicSkipTokenSource?.Cancel();   
    }
}