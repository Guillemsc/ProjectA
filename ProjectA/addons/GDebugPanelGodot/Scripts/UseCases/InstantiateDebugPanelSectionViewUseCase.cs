using GDebugPanelGodot.Datas;
using GDebugPanelGodot.DebugActions.Containers;
using GDebugPanelGodot.Extensions;
using GDebugPanelGodot.Views;
using Godot;

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

        void Toggle() => ToggleSectionViewCollapsedUseCase.Execute(debugPanelSectionView);

        debugPanelSectionView.Section = section;
        debugPanelSectionView.SectionButton!.ConnectButtonPressed(Toggle);
        debugPanelSectionView.SectionButton!.SetActiveCanvasItem(section.Collapsable);
        
        debugActionsData.SectionsViews.Add(section, debugPanelSectionView);
        
        RefreshSectionViewNameUseCase.Execute(debugPanelSectionView);
        SetSectionViewCollapsedUseCase.Execute(debugPanelSectionView, section.Collapsed);
    }
}