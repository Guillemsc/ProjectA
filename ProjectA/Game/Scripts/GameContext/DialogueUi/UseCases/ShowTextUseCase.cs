using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.DialogueUi.Data;
using Game.ServicesContext.Time.Services;
using Game.ServicesContext.Time.Tweens.Extensions;
using Godot;
using GTweens.Builders;
using GTweens.Easings;
using GTweensGodot.Extensions;
using GUtils.Locations.Enums;
using GUtilsGodot.Extensions;

namespace Game.GameContext.DialogueUi.UseCases;

public sealed class ShowTextUseCase
{
    readonly IGameTimesService _gameTimesService;
    readonly DialogueUiTweensData _dialogueUiTweensData;
    readonly DialoguePlayingData _dialoguePlayingData;
    readonly RichTextLabel _dialogueLabel;
    readonly Control _dialogueShownIndicatorControl;
    readonly AnimationPlayer _dialogueShownIndicatorAnimationPlayer;
    readonly float _dialogueDurationPerWord;

    public ShowTextUseCase(
        IGameTimesService gameTimesService,
        DialogueUiTweensData dialogueUiTweensData,
        DialoguePlayingData dialoguePlayingData,
        RichTextLabel dialogueLabel, 
        Control dialogueShownIndicatorControl, 
        AnimationPlayer dialogueShownIndicatorAnimationPlayer, 
        float dialogueDurationPerWord)
    {
        _gameTimesService = gameTimesService;
        _dialogueUiTweensData = dialogueUiTweensData;
        _dialoguePlayingData = dialoguePlayingData;
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
            .AppendCallback(() =>
            {
                _dialoguePlayingData.IsShowingText = true;
                
                _dialogueShownIndicatorControl.Visible = false;
            })
            .Append(_dialogueLabel.TweenDisplayedTextVisibleRatio(0f, 0f))
            .AppendCallback(() => _dialogueLabel.Text = $"[center]{text}")
            .Append(_dialogueLabel.TweenDisplayedTextVisibleRatio(1, duration).SetEasing(Easing.Linear))
            .AppendCallback(() => _dialogueShownIndicatorControl.Visible = true)
            .AppendCallback(() =>
            {
                _dialogueShownIndicatorAnimationPlayer.Reset();
                _dialogueShownIndicatorAnimationPlayer.Play("Idle");

                _dialoguePlayingData.IsShowingText = false;
            })
            .Build();
        
        _dialogueUiTweensData.CurrentDialogueTween.LinkTweenToGameTime(_gameTimesService.TimeContext);

        return _dialogueUiTweensData.CurrentDialogueTween.PlayAsync(cancellationToken);
    }
}