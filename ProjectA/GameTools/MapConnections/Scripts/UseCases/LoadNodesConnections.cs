using Game.GameContext.Connections.Enums;
using Game.GameContext.Connections.Views;
using GameTools.MapConnections.Scripts.Datas;
using GameTools.MapConnections.Scripts.Views;
using Godot;
using GUtils.Optionals;

namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class LoadNodesConnections
{
    readonly MapNodesData _mapNodesData;
    readonly GraphEdit _graphEdit;
    readonly GetNodeFromMapUseCase _getNodeFromMapUseCase;

    public LoadNodesConnections(
        MapNodesData mapNodesData, 
        GraphEdit graphEdit,
        GetNodeFromMapUseCase getNodeFromMapUseCase
        )
    {
        _mapNodesData = mapNodesData;
        _graphEdit = graphEdit;
        _getNodeFromMapUseCase = getNodeFromMapUseCase;
    }

    public void Execute()
    {
        foreach (MapConnectionNodeView node in _mapNodesData.MapConnectionNodeViews)
        {
            int fromIndex = 0;
            foreach (ConnectionView connectionView in node.MapView.ConnectionViews!)
            {
                if (connectionView.ConnectionType == ConnectionType.Enter)
                {
                    continue;
                }
                
                Optional<MapConnectionNodeView> optionalConnectedNode = _getNodeFromMapUseCase.Execute(connectionView.Map!);

                bool hasConnectedNode = optionalConnectedNode.TryGet(out MapConnectionNodeView connectedNode);

                if (!hasConnectedNode)
                {
                    continue;
                }

                int toIndex = 0;
                foreach (ConnectionView toConnection in connectedNode.MapView.ConnectionViews!)
                {
                    if (toConnection.ConnectionType == ConnectionType.Exit)
                    {
                        continue;
                    }
                    
                    bool isTo = toConnection.Uid!.Equals(connectionView.SpawnId)
                                && toConnection.SpawnId!.Equals(connectionView.Uid);

                    if (isTo)
                    {
                        _graphEdit.ConnectNode(node.Name, fromIndex, connectedNode.Name, toIndex);
                        break;
                    }
                    
                    ++toIndex;
                }

                ++fromIndex;
            }
        }
    }
}