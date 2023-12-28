using Game.GameContext.Cinematics.Datas;
using Game.GameContext.General.Configurations;
using Game.GameContext.Players.Views;

namespace Game.GameContext.Cinematics.Contexts;

public sealed class CinematicsContext
{
    public PlayerView PlayerView { get; }
    public GameConfiguration GameConfiguration { get; }
    public CinematicsMethods CinematicsMethods { get; }
    
    public CinematicsContext(
        PlayerView playerView,
        GameConfiguration gameConfiguration,
        CinematicsMethods cinematicsMethods
        )
    {
        PlayerView = playerView;
        GameConfiguration = gameConfiguration;
        CinematicsMethods = cinematicsMethods;
    }
}