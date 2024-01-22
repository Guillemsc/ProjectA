using System;
using GDebugPanelGodot.Extensions;
using Godot;

namespace GDebugPanelGodot.DebugActions.Widgets;

public partial class ButtonDebugActionWidget : Control
{
    [Export] public Label? Label;
    [Export] public Button? Button;
    
    public void Init(string name, Action action)
    {
        Label!.Text = name;
        Button!.ConnectButtonPressed(action);
    }
}