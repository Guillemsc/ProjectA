using GUtils.Executables;

namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class WhenSaveStateButtonUseCase : IExecutable
{
    readonly SaveNodesStateUseCase _saveNodesStateUseCase;

    public WhenSaveStateButtonUseCase(SaveNodesStateUseCase saveNodesStateUseCase)
    {
        _saveNodesStateUseCase = saveNodesStateUseCase;
    }

    public void Execute()
    {
        _saveNodesStateUseCase.Execute();
    }
}