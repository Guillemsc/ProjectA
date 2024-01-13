using System.IO;
using Game.GameContext.Connections.Enums;
using Game.GameContext.Connections.Views;
using Game.GameContext.Maps.Configurations;
using Game.GameContext.Maps.Views;
using GameTools.MapConnections.Scripts.Datas;
using GameTools.MapConnections.Scripts.Views;
using Godot;
using GUtilsGodot.Extensions;

namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class AddNodeUseCase
{
    readonly GraphEdit _graphEdit;
    readonly PackedScene _nodeViewPrefab;
    readonly MapNodesData _mapNodesData;

    public AddNodeUseCase(
        GraphEdit graphEdit, 
        PackedScene nodeViewPrefab, 
        MapNodesData mapNodesData
        )
    {
        _graphEdit = graphEdit;
        _nodeViewPrefab = nodeViewPrefab;
        _mapNodesData = mapNodesData;
    }

    public void Execute(MapConfiguration mapConfiguration, PackedScene mapPrefab, MapView mapView)
    {
        MapConnectionNodeView nodeInstance = _nodeViewPrefab.Instantiate<MapConnectionNodeView>();

        for (int i = 0; i < mapView.ConnectionViews!.Length; i++)
        {
            ConnectionView connectionView = mapView.ConnectionViews[i];
            
            bool isLeft = connectionView.ConnectionType == ConnectionType.Enter;
            
            nodeInstance.SetSlot(i, isLeft, 0, new Color(1, 0, 0),
                !isLeft, 0, new Color(1, 0, 0)
            );

            bool invalidName = string.IsNullOrEmpty(connectionView.ReadableName);
            
            Label slot = new Label();
            slot.Text = invalidName ? "?" : connectionView.ReadableName;
            slot.SetParent(nodeInstance);
        }
        
        Label description = new Label();
        description.Text = mapView.ReadableName;
        description.Modulate = new Color(0, 0, 1f);
        description.SetParent(nodeInstance);
        
        nodeInstance.SetParent(_graphEdit);

        nodeInstance.MapConfiguration = mapConfiguration;
        nodeInstance.MapPrefab = mapPrefab;
        nodeInstance.MapView = mapView;
        nodeInstance.Title = mapView.Name;
        
        _mapNodesData.MapConnectionNodeViews.Add(nodeInstance);
    }
}