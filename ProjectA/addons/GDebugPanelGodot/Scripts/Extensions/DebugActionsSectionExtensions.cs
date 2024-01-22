using System;
using GDebugPanelGodot.DebugActions.Actions;
using GDebugPanelGodot.DebugActions.Containers;

namespace GDebugPanelGodot.Extensions;

public static class DebugActionsSectionExtensions
{
    public static IDebugAction AddButton(this DebugActionsSection section, Action action)
    {
        IDebugAction debugAction = new ButtonDebugAction(action);
        section.Add(debugAction);
        return debugAction;
    }
}