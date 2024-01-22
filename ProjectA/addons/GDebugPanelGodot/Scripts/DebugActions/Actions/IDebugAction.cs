using GDebugPanelGodot.Views;
using Godot;

namespace GDebugPanelGodot.DebugActions.Actions;

public interface IDebugAction
{
    Control InstantiateWidget(DebugPanelView debugPanelView);
}