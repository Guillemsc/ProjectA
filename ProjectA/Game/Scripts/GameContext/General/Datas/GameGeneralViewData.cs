using Godot;

namespace Game.GameContext.General.Datas;

public sealed class GameGeneralViewData
{
    public Node Root { get; }

    public GameGeneralViewData(Node root)
    {
        Root = root;
    }
}