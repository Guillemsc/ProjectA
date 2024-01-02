using Game.ServicesContext.Music.Services;

namespace Game.GameContext.Cinematics.Datas;

public sealed class CinematicsServices
{
    public IMusicService MusicService { get; }
    
    public CinematicsServices(
        IMusicService musicService
        )
    {
        MusicService = musicService;
    }
}