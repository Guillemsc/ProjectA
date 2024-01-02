using Game.ServicesContext.Music.Enums;
using Godot;

namespace Game.ServicesContext.Music.Services;

public interface IMusicService
{
    void Play(AudioStream audioStream, float volume = 0, MusicFadeDuration duration = MusicFadeDuration.Medium);
    void Stop(MusicFadeDuration duration = MusicFadeDuration.Medium);
}