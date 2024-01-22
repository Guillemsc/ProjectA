using GDebugPanelGodot.Core;
using GDebugPanelGodot.DebugActions.Containers;
using GDebugPanelGodot.Extensions;
using Godot;

namespace GDebugPanelGodot.Nodes;

public partial class OpenGDebugPanelNode : Node
{
    [Export] public string Action = "ui_accept";
    
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed(Action))
        {
            Toggle();
        }
    }

    void Toggle()
    {
        if (GDebugPanel.IsVisible())
        {
            GDebugPanel.Hide();
        }
        else
        {
            DebugActionsSection section = GDebugPanel.AddSection("1");
            section.AddButton(() => GD.Print("A"));
            
            GDebugPanel.Show(this);
            
            DebugActionsSection section2 = GDebugPanel.AddSection("2");
            section2.AddButton(() => GD.Print("B"));
        }
    }
}