using Game.MetaContext.IntroUi.Datas;
using Godot;

namespace Game.MetaContext.IntroUi.UseCases;

public sealed class TickIntroInputForCancellationUseCase
{
    readonly IntroUiStopPlayingData _introUiStopPlayingData;

    public TickIntroInputForCancellationUseCase(
        IntroUiStopPlayingData introUiStopPlayingData
        )
    {
        _introUiStopPlayingData = introUiStopPlayingData;
    }

    public void Execute()
    {
        if (_introUiStopPlayingData.StopIntroCancellationTokenSource == null)
        {
            return;
        }
        
        bool cancel = Input.IsActionJustPressed("ui_accept");

        if (cancel)
        {
            _introUiStopPlayingData.StopIntroCancellationTokenSource.Cancel();   
        }
    }
}