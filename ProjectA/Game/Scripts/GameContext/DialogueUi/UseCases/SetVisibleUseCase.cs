using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.DialogueUi.Data;
using Godot;
using GUtilsGodot.Extensions;

namespace Game.GameContext.DialogueUi.UseCases;

public sealed class SetVisibleUseCase
{
    readonly DialogueUiVisibilityData _dialogueUiVisibilityData;
    readonly AnimationPlayer _animationPlayer;
    readonly ClearTextUseCase _clearTextUseCase;

    public SetVisibleUseCase(
        DialogueUiVisibilityData dialogueUiVisibilityData,
        AnimationPlayer animationPlayer, 
        ClearTextUseCase clearTextUseCase)
    {
        _dialogueUiVisibilityData = dialogueUiVisibilityData;
        _animationPlayer = animationPlayer;
        _clearTextUseCase = clearTextUseCase;
    }

    public Task Execute(bool visible, bool instantly, CancellationToken cancellationToken)
    {
        bool changed = _dialogueUiVisibilityData.Visible != visible;

        if (!changed)
        {
            return Task.CompletedTask;
        }

        _dialogueUiVisibilityData.Visible = visible;
        
        string animation = visible ? "Show" : "Hide";

        if (visible)
        {
            _clearTextUseCase.Execute();
        }

        return _animationPlayer.PlayAndAwaitCompletition(animation, instantly, cancellationToken);
    }
}