using GDebugPanelGodot.Datas;

namespace GDebugPanelGodot.UseCases;

public static class HasDebugPanelViewInstanceUseCase
{
    public static bool Execute(InstancesData instancesData)
    {
        return instancesData.DebugPanelView != null;
    }
}