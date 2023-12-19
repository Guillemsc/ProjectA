using System.Threading;
using System.Threading.Tasks;
using Godot;
using GTweens.Builders;
using GTweens.Tweens;
using GTweensGodot.Extensions;

namespace Game.LoadingScreenContext.MapTransitionUi.UseCases;

public sealed class SetVisibleUseCase
{
    readonly Control _pivot;
    readonly Control _panel;
    
    public SetVisibleUseCase(
        Control pivot,
        Control panel
        )
    {
        _pivot = pivot;
        _panel = panel;
    }
    
    public Task Execute(bool visible, bool instantly, CancellationToken cancellationToken)
    {
        Vector2 origin = visible ? new Vector2(-_panel.Size.X, 0) : Vector2.Zero;
        Vector2 destination = visible ? Vector2.Zero : new Vector2(_panel.Size.X, 0);

        GTweenSequenceBuilder builder = GTweenSequenceBuilder.New()
            .Append(_panel.TweenPosition(origin, 0f))
            .AppendCallback(() => _pivot.Visible = true)
            .Append(_panel.TweenPosition(destination, 0.35f))
            .AppendCallback(() => _pivot.Visible = visible);

        GTween tween = builder.Build();
        
        return tween.PlayAsync(instantly, cancellationToken);
    }
}