using GameTools.MapConnections.Scripts.Datas;
using GameTools.MapConnections.Scripts.Views;
using GUtils.Optionals;

namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class GetNodeByNameUseCase
{
    readonly MapNodesData _mapNodesData;

    public GetNodeByNameUseCase(MapNodesData mapNodesData)
    {
        _mapNodesData = mapNodesData;
    }

    public Optional<MapConnectionNodeView> Execute(string name)
    {
        MapConnectionNodeView? node = _mapNodesData.MapConnectionNodeViews.Find(i => i.Name.Equals(name));
        
        return Optional<MapConnectionNodeView>.Some(node);
    }
}