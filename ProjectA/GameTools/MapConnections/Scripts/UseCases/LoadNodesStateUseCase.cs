using GameTools.MapConnections.Scripts.Configurations;
using GameTools.MapConnections.Scripts.Datas;
using GameTools.MapConnections.Scripts.Views;
using Godot;
using Newtonsoft.Json;

namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class LoadNodesStateUseCase
{
    readonly MapConnectionsToolNodeConfiguration _mapConnectionsToolNodeConfiguration;
    readonly MapNodesData _mapNodesData;

    public LoadNodesStateUseCase(
        MapConnectionsToolNodeConfiguration mapConnectionsToolNodeConfiguration, 
        MapNodesData mapNodesData
        )
    {
        _mapConnectionsToolNodeConfiguration = mapConnectionsToolNodeConfiguration;
        _mapNodesData = mapNodesData;
    }

    public void Execute()
    {
        NodesSaveData? saveData = JsonConvert.DeserializeObject<NodesSaveData>(_mapConnectionsToolNodeConfiguration.Json!);

        if (saveData == null)
        {
            return;
        }

        foreach (NodeSaveData nodeSaveData in saveData.Nodes)
        {
            MapConnectionNodeView? mapConnectionNodeView = _mapNodesData.MapConnectionNodeViews.Find(
                e => e.MapPrefab.ResourcePath.Equals(nodeSaveData.MapName)
            );

            if (mapConnectionNodeView == null)
            {
                continue;
            }

            mapConnectionNodeView.PositionOffset = new Vector2(
                nodeSaveData.PositionX,
                nodeSaveData.PositionY
            );
        }
    }
}