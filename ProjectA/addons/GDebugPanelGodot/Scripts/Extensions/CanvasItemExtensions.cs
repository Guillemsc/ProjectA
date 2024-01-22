using Godot;

namespace GDebugPanelGodot.Extensions;

public static class CanvasItemExtensions
{
    public static void SetActiveCanvasItem(this CanvasItem canvasItem, bool active)
    {
        canvasItem.ProcessMode = active ? Node.ProcessModeEnum.Inherit : Node.ProcessModeEnum.Disabled;
        canvasItem.Visible = active;
    }
}