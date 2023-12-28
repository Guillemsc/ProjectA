using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.DialogueUi.Data;
using Godot;
using GTweens.Builders;
using GTweens.Easings;
using GTweensGodot.Extensions;
using GUtils.Locations.Enums;
using GUtilsGodot.Extensions;

namespace Game.GameContext.DialogueUi.UseCases;

public sealed class ShowTextUseCase
{
    readonly DialogueUiTweensData _dialogueUiTweensData;
    readonly RichTextLabel _dialogueLabel;
    readonly Control _dialogueShownIndicatorControl;
    readonly AnimationPlayer _dialogueShownIndicatorAnimationPlayer;
    readonly float _dialogueDurationPerWord;

    public ShowTextUseCase(
        DialogueUiTweensData dialogueUiTweensData,
        RichTextLabel dialogueLabel, 
        Control dialogueShownIndicatorControl, 
        AnimationPlayer dialogueShownIndicatorAnimationPlayer, 
        float dialogueDurationPerWord
        )
    {
        _dialogueUiTweensData = dialogueUiTweensData;
        _dialogueLabel = dialogueLabel;
        _dialogueShownIndicatorControl = dialogueShownIndicatorControl;
        _dialogueShownIndicatorAnimationPlayer = dialogueShownIndicatorAnimationPlayer;
        _dialogueDurationPerWord = dialogueDurationPerWord;
    }

    public Task Execute(string text, CancellationToken cancellationToken)
    {
        float duration = text.Length * _dialogueDurationPerWord;
        
        _dialogueUiTweensData.CurrentDialogueTween?.Kill();
        
        _dialogueUiTweensData.CurrentDialogueTween = GTweenSequenceBuilder.New()
            .AppendCallback(() => _dialogueShownIndicatorControl.Visible = false)
            .Append(_dialogueLabel.TweenDisplayedTextVisibleRatio(0f, 0f))
            .AppendCallback(() => _dialogueLabel.Text = $"[center]{text}")
            .Append(_dialogueLabel.TweenDisplayedTextVisibleRatio(1, duration).SetEasing(Easing.Linear))
            .AppendCallback(() => _dialogueShownIndicatorControl.Visible = true)
            .AppendCallback(() =>
            {
                _dialogueShownIndicatorAnimationPlayer.Reset();
                _dialogueShownIndicatorAnimationPlayer.Play("Idle");
            })
            .Build();

        return _dialogueUiTweensData.CurrentDialogueTween.PlayAsync(cancellationToken);
    }
}