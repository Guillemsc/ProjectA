using System.Threading;

namespace Game.MetaContext.IntroUi.Datas;

public sealed class IntroUiStopPlayingData
{
    public CancellationTokenSource? StopIntroCancellationTokenSource = null;
}