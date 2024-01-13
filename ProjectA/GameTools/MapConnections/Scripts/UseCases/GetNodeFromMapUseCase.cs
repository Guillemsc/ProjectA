using GameTools.MapConnections.Scripts.Datas;
using GameTools.MapConnections.Scripts.Views;
using GUtils.Optionals;

namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class GetNodeFromMapUseCase
{
    readonly MapNodesData _mapNodesData;

    public GetNodeFromMapUseCase(
        MapNodesData mapNodesData
    )
    {
        _mapNodesData = mapNodesData;
    }
    
    public Optional<MapConnectionNodeView> Execute(string map)
    {
        foreach (MapConnectionNodeView node in _mapNodesData.MapConnectionNodeViews)
        {
            bool isMap = node.MapConfiguration.Map!.Equals(map);

            if (isMap)
            {
                return node;
            }
        }
        
        return Optional<MapConnectionNodeView>.None;
    }
}