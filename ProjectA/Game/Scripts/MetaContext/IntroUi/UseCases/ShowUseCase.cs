using System.Threading;
using System.Threading.Tasks;
using Game.MetaContext.IntroUi.Datas;
using Godot;
using GUtilsGodot.Extensions;

namespace Game.MetaContext.IntroUi.UseCases;

public sealed class ShowUseCase
{
    readonly AnimationPlayer _animationPlayer;
    readonly IntroUiStopPlayingData _introUiStopPlayingData;

    public ShowUseCase(
        AnimationPlayer animationPlayer, 
        IntroUiStopPlayingData introUiStopPlayingData
        )
    {
        _animationPlayer = animationPlayer;
        _introUiStopPlayingData = introUiStopPlayingData;
    }

    public async Task Execute(bool instantly, CancellationToken cancellationToken)
    {
        _introUiStopPlayingData.StopIntroCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        await _animationPlayer.PlayAndAwaitCompletition(
            "Show",
            instantly,
            _introUiStopPlayingData.StopIntroCancellationTokenSource.Token
        );
        
        _introUiStopPlayingData.StopIntroCancellationTokenSource = null;
    }
}