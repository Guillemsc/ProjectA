using GDebugPanelGodot.Datas;
using GDebugPanelGodot.DebugActions.Actions;
using GDebugPanelGodot.DebugActions.Containers;

namespace GDebugPanelGodot.UseCases;

public static class RemoveDebugActionsSectionUseCase
{
    public static void Execute(DebugActionsData debugActionsData, DebugActionsSection section)
    {
        bool found = debugActionsData.Sections.Remove(section);

        if (!found)
        {
            return;
        }
        
        foreach (IDebugAction debugAction in section.Actions)
        {
            RemoveDebugActionWidgetUseCase.Execute(debugActionsData, debugAction);
        }
    }
}