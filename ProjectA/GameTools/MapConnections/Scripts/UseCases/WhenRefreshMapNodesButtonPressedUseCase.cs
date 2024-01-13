using GUtils.Executables;

namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class WhenRefreshMapNodesButtonPressedUseCase : IExecutable
{
    readonly RefreshMapNodesUseCase _refreshMapNodesUseCase;

    public WhenRefreshMapNodesButtonPressedUseCase(
        RefreshMapNodesUseCase refreshMapNodesUseCase
        )
    {
        _refreshMapNodesUseCase = refreshMapNodesUseCase;
    }

    public void Execute()
    {
        _refreshMapNodesUseCase.Execute();
    }
}