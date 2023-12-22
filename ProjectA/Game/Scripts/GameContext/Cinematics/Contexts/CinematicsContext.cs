using Game.GameContext.Cinematics.Datas;
using Game.GameContext.Players.Views;

namespace Game.GameContext.Cinematics.Contexts;

public sealed class CinematicsContext
{
    public PlayerView PlayerView { get; }
    public CinematicsMethods CinematicsMethods { get; }
    
    public CinematicsContext(
        PlayerView playerView,
        CinematicsMethods cinematicsMethods
        )
    {
        PlayerView = playerView;
        CinematicsMethods = cinematicsMethods;
    }
}