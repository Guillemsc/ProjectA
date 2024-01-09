using Game.ServicesContext.Music.Services;
using Game.ServicesContext.Time.Services;

namespace Game.GameContext.Cinematics.Datas;

public sealed class CinematicsServices
{
    public IGameTimesService GameTimesService { get; }
    public IMusicService MusicService { get; }
    
    public CinematicsServices(
        IGameTimesService gameTimesService,
        IMusicService musicService
        )
    {
        GameTimesService = gameTimesService;
        MusicService = musicService;
    }
}