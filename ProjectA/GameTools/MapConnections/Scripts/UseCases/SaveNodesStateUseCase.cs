using GameTools.MapConnections.Scripts.Configurations;
using GameTools.MapConnections.Scripts.Datas;
using GameTools.MapConnections.Scripts.Views;
using GUtilsGodot.Extensions;
using Newtonsoft.Json;

namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class SaveNodesStateUseCase
{
    readonly MapNodesData _mapNodesData;
    readonly MapConnectionsToolNodeConfiguration _mapConnectionsToolNodeConfiguration;

    public SaveNodesStateUseCase(
        MapNodesData mapNodesData, 
        MapConnectionsToolNodeConfiguration mapConnectionsToolNodeConfiguration
        )
    {
        _mapNodesData = mapNodesData;
        _mapConnectionsToolNodeConfiguration = mapConnectionsToolNodeConfiguration;
    }

    public void Execute()
    {
        NodesSaveData nodesSaveData = new();
        
        foreach (MapConnectionNodeView nodeView in _mapNodesData.MapConnectionNodeViews)
        {
            NodeSaveData nodeSaveData = new()
            {
                MapName = nodeView.MapPrefab.ResourcePath,
                PositionX = nodeView.PositionOffset.X,
                PositionY = nodeView.PositionOffset.Y
            };
            
            nodesSaveData.Nodes.Add(nodeSaveData);
        }   
        
        string json = JsonConvert.SerializeObject(nodesSaveData);

        _mapConnectionsToolNodeConfiguration.Json = json;
        _mapConnectionsToolNodeConfiguration.Save();
    }
}