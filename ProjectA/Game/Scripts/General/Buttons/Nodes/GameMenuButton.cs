using Game.General.Buttons.Configurations;
using Godot;
using GTweens.Builders;
using GTweens.Tweens;
using GTweensGodot.Extensions;
using GUtilsGodot.Extensions;

namespace Game.General.Buttons.Nodes;

public partial class GameMenuButton : Button
{
    [Export] GameMenuButtonConfiguration? Configuration;
    
    bool _canPlayAnimation = true;
    GTween? _tween;
    
    public override void _Ready()
    {
        this.ConnectControlFocusEntered(WhenFocusEntered);
    }

    public void GameMenuButtonGrabFocus()
    {
        _canPlayAnimation = false;
        GrabFocus();
        _canPlayAnimation = true;
    }
    
    void WhenFocusEntered()
    {
        PlayShakeAnimation();
    }

    void PlayShakeAnimation()
    {
        if (!_canPlayAnimation)
        {
            return;
        }
        
        _tween?.Complete();

        _tween = GTweenSequenceBuilder.New()
            .Append(this.TweenPositionX(Configuration!.Offset, Configuration.Duration)
                .SetEasing(Configuration!.Curve!))
            .Build();
        
        _tween.Play();
    }
}