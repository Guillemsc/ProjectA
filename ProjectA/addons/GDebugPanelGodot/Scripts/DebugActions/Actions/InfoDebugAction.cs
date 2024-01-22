using GDebugPanelGodot.DebugActions.Widgets;
using GDebugPanelGodot.Views;
using Godot;

namespace GDebugPanelGodot.DebugActions.Actions;

public sealed class InfoDebugAction : IDebugAction
{
    public string Info { get; }
    
    public InfoDebugAction(string info)
    {
        Info = info;
    }
    
    public Control InstantiateWidget(DebugPanelView debugPanelView)
    {
        InfoDebugActionWidget widget = debugPanelView.InfoDebugActionWidget!.Instantiate<InfoDebugActionWidget>();
        widget.Init(Info);
        return widget;
    }
}