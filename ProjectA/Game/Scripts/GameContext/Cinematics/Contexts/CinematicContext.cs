using Game.GameContext.Cinematics.Datas;
using Game.GameContext.General.Configurations;
using Game.GameContext.Players.Views;

namespace Game.GameContext.Cinematics.Contexts;

public sealed class CinematicContext
{
    public PlayerView PlayerView { get; }
    public GameConfiguration GameConfiguration { get; }
    public CinematicsMethods Methods { get; }
    public CinematicsServices Services { get; }
    
    public CinematicContext(
        PlayerView playerView,
        GameConfiguration gameConfiguration,
        CinematicsMethods methods, 
        CinematicsServices services
        )
    {
        PlayerView = playerView;
        GameConfiguration = gameConfiguration;
        Methods = methods;
        Services = services;
    }
}