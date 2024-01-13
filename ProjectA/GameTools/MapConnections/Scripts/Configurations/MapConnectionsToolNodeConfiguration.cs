using Godot;

namespace GameTools.MapConnections.Scripts.Configurations;

[GlobalClass]
public partial class MapConnectionsToolNodeConfiguration : Resource
{
    [Export] public string? Json;
}