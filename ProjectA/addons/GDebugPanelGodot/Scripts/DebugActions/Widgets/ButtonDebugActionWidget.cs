using System;
using GDebugPanelGodot.Extensions;
using Godot;

namespace GDebugPanelGodot.DebugActions.Widgets;

public partial class ButtonDebugActionWidget : Button
{
    public void Init(Action action)
    {
       this.ConnectButtonPressed(action);
    }
}