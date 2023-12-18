using Game.GameContext.Connections.Views;
using GUtils.Optionals;

namespace Game.GameContext.Connections.Datas;

public sealed class ConnectionsData
{
    public Optional<ConnectionView> CurrentConnection;
    public bool HasIgnoredFirstConnectionCollision;
}