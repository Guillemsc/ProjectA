using GDebugPanelGodot.Datas;
using GDebugPanelGodot.DebugActions.Actions;
using GDebugPanelGodot.DebugActions.Containers;

namespace GDebugPanelGodot.UseCases;

public static class CreateDebugActionsWidgetsUseCase
{
    public static void Execute(InstancesData instancesData, DebugActionsData debugActionsData)
    {
        foreach (DebugActionsSection section in debugActionsData.Sections)
        {
            InstantiateDebugPanelSectionViewUseCase.Execute(instancesData, debugActionsData, section);
            
            foreach (IDebugAction debugAction in section.Actions)
            {
                CreateDebugActionWidgetUseCase.Execute(instancesData, debugActionsData, section, debugAction);
            }
        }
        
        ReorderSectionViewsByPriorityUseCase.Execute(debugActionsData);
    }
}