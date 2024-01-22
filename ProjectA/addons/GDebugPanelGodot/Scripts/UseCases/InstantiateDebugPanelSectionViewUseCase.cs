using GDebugPanelGodot.Datas;
using GDebugPanelGodot.DebugActions.Containers;
using GDebugPanelGodot.Extensions;
using GDebugPanelGodot.Views;

namespace GDebugPanelGodot.UseCases;

public static class InstantiateDebugPanelSectionViewUseCase
{
    public static void Execute(InstancesData instancesData, DebugActionsData debugActionsData, DebugActionsSection section)
    {
        if (instancesData.DebugPanelView == null)
        {
            return;
        }

        bool alreadyExists = debugActionsData.SectionsViews.ContainsKey(section);

        if (alreadyExists)
        {
            return;
        }

        DebugPanelSectionView debugPanelSectionView = instancesData.DebugPanelView!.DebugPanelSection!.Instantiate<DebugPanelSectionView>();
        debugPanelSectionView.SetParent(instancesData.DebugPanelView.ContentVBox!);

        debugPanelSectionView.NameLabel!.Text = section.Name;
        
        debugActionsData.SectionsViews.Add(section, debugPanelSectionView);
    }
}