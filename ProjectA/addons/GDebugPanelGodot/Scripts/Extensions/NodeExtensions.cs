using Godot;

namespace GDebugPanelGodot.Extensions;

public static class NodeExtensions
{
    public static void RemoveParent(this Node node)
    {
        Node parent = node.GetParent();

        if (parent == null)
        {
            return;
        }
        
        parent.RemoveChild(node);
    }

    public static void SetParent(this Node node, Node parent)
    {
        node.RemoveParent();
        parent.AddChild(node);
    }
}