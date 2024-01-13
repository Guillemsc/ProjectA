using GUtils.Executables;

namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class WhenRefreshMapListButtonPressedUseCase : IExecutable
{
    readonly RefreshMapsListUseCase _refreshMapsListUseCase;

    public WhenRefreshMapListButtonPressedUseCase(
        RefreshMapsListUseCase refreshMapsListUseCase
        )
    {
        _refreshMapsListUseCase = refreshMapsListUseCase;
    }

    public void Execute()
    {
        _refreshMapsListUseCase.Execute();
    }
}