using Game.ServicesContext.Music.Enums;
using Godot;
using GTweens.Builders;
using GTweens.Easings;
using GTweens.Tweens;
using GTweensGodot.Extensions;

namespace Game.ServicesContext.Music.Services;

public partial class MusicService : Node, IMusicService
{
    [Export] public AudioStreamPlayer2D? AudioStreamPlayer;
    
    public void Play(AudioStream audioStream, float volume = 0, MusicFadeDuration duration = MusicFadeDuration.Medium)
    {
        float durationSeconds = GetDuration(duration);
        
        GTweenSequenceBuilder builder = GTweenSequenceBuilder.New();
        
        if (AudioStreamPlayer!.Playing)
        {
            builder.Append(AudioStreamPlayer.TweenVolumeDb(-80f, durationSeconds).SetEasing(Easing.InQuad));
        }

        builder.AppendCallback(() =>
        {
            AudioStreamPlayer.Stream = audioStream;
            
            AudioStreamPlayer.Play();
        });
        
        builder.Append(AudioStreamPlayer.TweenVolumeDb(volume, durationSeconds).SetEasing(Easing.OutQuad));

        GTween tween = builder.Build();
        
        tween.Play();
    }

    public void Stop(MusicFadeDuration duration = MusicFadeDuration.Medium)
    {
        float durationSeconds = GetDuration(duration);
        
        GTweenSequenceBuilder builder = GTweenSequenceBuilder.New();
        
        builder.Append(AudioStreamPlayer!.TweenVolumeDb(-80, durationSeconds).SetEasing(Easing.InQuad));
        
        GTween tween = builder.Build();
        
        tween.Play();
    }

    float GetDuration(MusicFadeDuration duration)
    {
        switch (duration)
        {
            case MusicFadeDuration.Short:
            {
                return 0.5f;
            }
            
            default:
            case MusicFadeDuration.Medium:
            {
                return 1.0f;
            }
            
            case MusicFadeDuration.Long:
            {
                return 2.0f;
            }
        }
    }
}