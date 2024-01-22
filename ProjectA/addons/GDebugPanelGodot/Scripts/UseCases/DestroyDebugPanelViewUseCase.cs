using GDebugPanelGodot.Datas;
using GDebugPanelGodot.Extensions;

namespace GDebugPanelGodot.UseCases;

public static class DestroyDebugPanelViewUseCase
{
    public static void Execute(InstancesData instancesData)
    {
        if (instancesData.DebugPanelView == null)
        {
            return;
        }
        
        instancesData.DebugPanelView.RemoveParent();
        instancesData.DebugPanelView.QueueFree();

        instancesData.DebugPanelView = null;
    }
}