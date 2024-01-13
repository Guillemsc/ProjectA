using Game.GameContext.Maps.Configurations;
using Game.GameContext.Maps.Views;
using Godot;

namespace GameTools.MapConnections.Scripts.Views;

public partial class MapConnectionNodeView : GraphNode
{
    public PackedScene MapPrefab;
    public MapView MapView;
    public MapConfiguration MapConfiguration;
}