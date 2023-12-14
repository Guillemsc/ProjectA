using Godot;

namespace Game.GameContext.General.Datas;

public sealed class GameGeneralViewData
{
    public Node PlayerParent { get; }

    public GameGeneralViewData(Node playerParent)
    {
        PlayerParent = playerParent;
    }
}