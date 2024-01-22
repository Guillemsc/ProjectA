using System;
using GDebugPanelGodot.DebugActions.Widgets;
using GDebugPanelGodot.Views;
using Godot;

namespace GDebugPanelGodot.DebugActions.Actions;

public sealed class ButtonDebugAction : IDebugAction
{
    public Action Action { get; }

    public ButtonDebugAction(Action action)
    {
        Action = action;
    }

    public Control InstantiateWidget(DebugPanelView debugPanelView)
    {
        ButtonDebugActionWidget widget = debugPanelView.ButtonDebugActionWidget!.Instantiate<ButtonDebugActionWidget>();
        widget.Init(Action);
        return widget;
    }
}