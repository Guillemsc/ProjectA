using GDebugPanelGodot.Datas;
using GDebugPanelGodot.DebugActions.Actions;
using GDebugPanelGodot.DebugActions.Containers;
using GDebugPanelGodot.Views;
using Godot;
using GUtilsGodot.Extensions;

namespace GDebugPanelGodot.UseCases;

public static class CreateDebugActionWidgetUseCase
{
    public static void Execute(
        InstancesData instancesData, 
        DebugActionsData debugActionsData, 
        DebugActionsSection section,
        IDebugAction debugAction
        )
    {
        bool hasInstance = instancesData.DebugPanelView != null;

        if (!hasInstance)
        {
            return;
        }

        bool sectionViewExists = debugActionsData.SectionsViews.TryGetValue(section, out DebugPanelSectionView? sectionView);

        if (!sectionViewExists)
        {
            return;
        }
        
        bool alreadyExists = debugActionsData.Widgets.ContainsKey(debugAction);

        if (alreadyExists)
        {
            return;
        }

        Control widget = debugAction.InstantiateWidget(instancesData.DebugPanelView!);
        widget.SetParent(sectionView!.ContentParent!);
        
        debugActionsData.Widgets.Add(debugAction, widget);
    }
}