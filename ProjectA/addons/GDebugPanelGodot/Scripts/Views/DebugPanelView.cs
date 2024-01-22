using Godot;

namespace GDebugPanelGodot.Views;

public partial class DebugPanelView : Control
{
    [Export] public Control? ContentVBox;
    [Export] public PackedScene? DebugPanelSection;
    [Export] public PackedScene? ButtonDebugActionWidget;
}