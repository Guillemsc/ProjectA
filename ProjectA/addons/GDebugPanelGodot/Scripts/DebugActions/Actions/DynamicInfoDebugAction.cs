using System;
using GDebugPanelGodot.DebugActions.Widgets;
using GDebugPanelGodot.Views;
using Godot;

namespace GDebugPanelGodot.DebugActions.Actions;

public sealed class DynamicInfoDebugAction : IDebugAction
{
    public Func<string> GetInfoAction { get; }
    
    public DynamicInfoDebugAction(Func<string> getInfoAction)
    {
        GetInfoAction = getInfoAction;
    }
    
    public Control InstantiateWidget(DebugPanelView debugPanelView)
    {
        DynamicInfoDebugActionWidget widget = debugPanelView.DynamicInfoDebugActionWidget!.Instantiate<DynamicInfoDebugActionWidget>();
        widget.Init(GetInfoAction);
        return widget;
    }
}