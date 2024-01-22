using GDebugPanelGodot.Datas;
using GDebugPanelGodot.DebugActions.Actions;
using GDebugPanelGodot.DebugActions.Containers;

namespace GDebugPanelGodot.UseCases;

public static class AddDebugActionsSectionUseCase
{
    public static DebugActionsSection Execute(
        InstancesData instancesData, 
        DebugActionsData debugActionsData, 
        string name,
        int priority = 0
        )
    {
        void Add(DebugActionsSection debugActionSection, IDebugAction debugAction)
            => CreateDebugActionWidgetUseCase.Execute(instancesData, debugActionsData, debugActionSection, debugAction);

        void Remove(IDebugAction debugAction)
            => RemoveDebugActionWidgetUseCase.Execute(debugActionsData, debugAction);
        
        DebugActionsSection section = new(name, priority, Add, Remove);
        
        debugActionsData.Sections.Add(section);
        
        InstantiateDebugPanelSectionViewUseCase.Execute(instancesData, debugActionsData, section);

        return section;
    }
}