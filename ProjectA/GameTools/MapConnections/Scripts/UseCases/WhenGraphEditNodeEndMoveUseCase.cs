namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class WhenGraphEditNodeEndMoveUseCase
{
    readonly SaveNodesStateUseCase _saveNodesStateUseCase;

    public WhenGraphEditNodeEndMoveUseCase(
        SaveNodesStateUseCase saveNodesStateUseCase
        )
    {
        _saveNodesStateUseCase = saveNodesStateUseCase;
    }

    public void Execute()
    {
        _saveNodesStateUseCase.Execute();
    }
}