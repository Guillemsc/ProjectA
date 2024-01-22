using System;
using System.Collections.Generic;
using GDebugPanelGodot.DebugActions.Actions;

namespace GDebugPanelGodot.DebugActions.Containers;

public sealed class DebugActionsSection
{
    readonly List<IDebugAction> _actions = new();
    
    readonly Action<DebugActionsSection, IDebugAction> _addAction;
    readonly Action<IDebugAction> _removeAction;
    
    public string Name { get; }
    public IReadOnlyList<IDebugAction> Actions => _actions;
    
    public DebugActionsSection(
        string name, 
        Action<DebugActionsSection, IDebugAction> addAction, 
        Action<IDebugAction> removeAction
        )
    {
        Name = name;
        _addAction = addAction;
        _removeAction = removeAction;
    }
    
    public void Add(IDebugAction action)
    {
        _actions.Add(action);
        _addAction.Invoke(this, action);
    }

    public void Remove(IDebugAction action)
    {
        bool found = _actions.Remove(action);

        if (!found)
        {
            return;
        }
        
        _removeAction.Invoke(action);
    }
}