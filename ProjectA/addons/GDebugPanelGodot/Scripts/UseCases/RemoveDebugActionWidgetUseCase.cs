using GDebugPanelGodot.Datas;
using GDebugPanelGodot.DebugActions.Actions;
using GDebugPanelGodot.Extensions;
using Godot;

namespace GDebugPanelGodot.UseCases;

public static class RemoveDebugActionWidgetUseCase
{
    public static void Execute(DebugActionsData debugActionsData, IDebugAction debugAction)
    {
        bool hasWidget = debugActionsData.Widgets.TryGetValue(debugAction, out Control? widget);

        if (!hasWidget)
        {
            return;
        }
        
        widget!.RemoveParent();
        widget!.QueueFree();

        debugActionsData.Widgets.Remove(debugAction);
    }
}