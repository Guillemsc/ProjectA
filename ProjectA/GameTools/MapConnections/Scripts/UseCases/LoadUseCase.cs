namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class LoadUseCase
{
    readonly RefreshMapNodesUseCase _refreshMapNodesUseCase;
    readonly CheckThatAllConnectionsHaveUidUseCase _checkThatAllConnectionsHaveUidUseCase;
    readonly LoadNodesStateUseCase _loadNodesStateUseCase;
    readonly LoadNodesConnections _loadNodesConnections;

    public LoadUseCase(
        RefreshMapNodesUseCase refreshMapNodesUseCase,
        CheckThatAllConnectionsHaveUidUseCase checkThatAllConnectionsHaveUidUseCase,
        LoadNodesStateUseCase loadNodesStateUseCase, 
        LoadNodesConnections loadNodesConnections
        )
    {
        _refreshMapNodesUseCase = refreshMapNodesUseCase;
        _checkThatAllConnectionsHaveUidUseCase = checkThatAllConnectionsHaveUidUseCase;
        _loadNodesStateUseCase = loadNodesStateUseCase;
        _loadNodesConnections = loadNodesConnections;
    }

    public void Execute()
    {
        _refreshMapNodesUseCase.Execute();
        _checkThatAllConnectionsHaveUidUseCase.Execute();
        _loadNodesStateUseCase.Execute();
        _loadNodesConnections.Execute();
    }
}