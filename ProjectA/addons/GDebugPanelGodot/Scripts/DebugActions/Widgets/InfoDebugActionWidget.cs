using Godot;

namespace GDebugPanelGodot.DebugActions.Widgets;

public partial class InfoDebugActionWidget : Control
{
    [Export] public Label? Label;

    public void Init(string info)
    {
        Label!.Text = info;
    }
}