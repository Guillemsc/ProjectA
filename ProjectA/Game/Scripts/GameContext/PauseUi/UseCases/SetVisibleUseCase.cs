using System.Threading;
using System.Threading.Tasks;
using Godot;
using GUtilsGodot.Extensions;

namespace Game.GameContext.PauseUi.UseCases;

public sealed class SetVisibleUseCase
{
    readonly AnimationPlayer _animationPlayer;
    readonly SetInitialFocusUseCase _setInitialFocusUseCase;

    public SetVisibleUseCase(
        AnimationPlayer animationPlayer, 
        SetInitialFocusUseCase setInitialFocusUseCase
        )
    {
        _animationPlayer = animationPlayer;
        _setInitialFocusUseCase = setInitialFocusUseCase;
    }
    
    public Task Execute(bool visible, bool instantly, CancellationToken cancellationToken)
    {
        string animation = visible ? "Show" : "Hide";

        if (visible)
        {
            _setInitialFocusUseCase.Execute();
        }
        
        return _animationPlayer.PlayAndAwaitCompletition(animation, instantly, cancellationToken);
    }
}