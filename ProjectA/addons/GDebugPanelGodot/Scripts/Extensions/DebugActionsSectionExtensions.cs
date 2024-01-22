using System;
using GDebugPanelGodot.DebugActions.Actions;
using GDebugPanelGodot.DebugActions.Containers;

namespace GDebugPanelGodot.Extensions;

public static class DebugActionsSectionExtensions
{
    public static IDebugAction AddInfo(this DebugActionsSection section, string info)
    {
        IDebugAction debugAction = new InfoDebugAction(info);
        section.Add(debugAction);
        return debugAction;
    }
    
    public static IDebugAction AddInfoDynamic(this DebugActionsSection section, Func<string> getInfoAction)
    {
        IDebugAction debugAction = new DynamicInfoDebugAction(getInfoAction);
        section.Add(debugAction);
        return debugAction;
    }
    
    public static IDebugAction AddButton(this DebugActionsSection section, string name, Action action)
    {
        IDebugAction debugAction = new ButtonDebugAction(name, action);
        section.Add(debugAction);
        return debugAction;
    }
    
    public static IDebugAction AddToggle(this DebugActionsSection section, string name, Action<bool> setAction, Func<bool> getAction)
    {
        IDebugAction debugAction = new ToggleDebugAction(name, setAction, getAction);
        section.Add(debugAction);
        return debugAction;
    }
    
    public static IDebugAction AddInt(this DebugActionsSection section, string name, int step, Action<int> setAction, Func<int> getAction)
    {
        IDebugAction debugAction = new IntDebugAction(name, step, setAction, getAction);
        section.Add(debugAction);
        return debugAction;
    }
    
    public static IDebugAction AddInt(this DebugActionsSection section, string name, Action<int> setAction, Func<int> getAction)
    {
        return section.AddInt(name, 1, setAction, getAction);
    }
    
    public static IDebugAction AddFloat(this DebugActionsSection section, string name, float step, Action<float> setAction, Func<float> getAction)
    {
        IDebugAction debugAction = new FloatDebugAction(name, step, setAction, getAction);
        section.Add(debugAction);
        return debugAction;
    }
    
    public static IDebugAction AddFloat(this DebugActionsSection section, string name, Action<float> setAction, Func<float> getAction)
    {
        return section.AddFloat(name, 0.1f, setAction, getAction);
    }
}