using System.Collections.Generic;
using GDebugPanelGodot.DebugActions.Actions;
using GDebugPanelGodot.DebugActions.Containers;
using GDebugPanelGodot.Views;
using Godot;

namespace GDebugPanelGodot.Datas;

public sealed class DebugActionsData
{
    public readonly List<DebugActionsSection> Sections = new();
    public readonly Dictionary<DebugActionsSection, DebugPanelSectionView> SectionsViews = new();
    public readonly Dictionary<IDebugAction, Control> Widgets = new();
}