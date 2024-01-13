using System;
using Game.GameContext.Maps.Views;
using GameTools.MapConnections.Scripts.Views;
using Godot;
using GUtils.Optionals;

namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class WhenGraphEditConnectionRequestUseCase
{
    readonly GraphEdit _graphEdit;
    readonly GetNodeByNameUseCase _getNodeByNameUseCase;
    readonly ConnectMapsUseCase _connectMapsUseCase;

    public WhenGraphEditConnectionRequestUseCase(
        GraphEdit graphEdit, 
        GetNodeByNameUseCase getNodeByNameUseCase, 
        ConnectMapsUseCase connectMapsUseCase
        )
    {
        _graphEdit = graphEdit;
        _getNodeByNameUseCase = getNodeByNameUseCase;
        _connectMapsUseCase = connectMapsUseCase;
    }

    public void Execute(string fromNode, int fromPort, string toNode, int toPort)
    {
        Optional<MapConnectionNodeView> optionalFromNode = _getNodeByNameUseCase.Execute(fromNode);

        bool fromNodeFound = optionalFromNode.TryGet(out MapConnectionNodeView fromNodeView);

        if (!fromNodeFound)
        {
            return;
        }
        
        Optional<MapConnectionNodeView> optionalToNode = _getNodeByNameUseCase.Execute(toNode);

        bool toNodeFound = optionalToNode.TryGet(out MapConnectionNodeView toNodeView);

        if (!toNodeFound)
        {
            return;
        }

        bool couldConnect = _connectMapsUseCase.Execute(
            new Tuple<PackedScene, MapView>(fromNodeView.MapPrefab, fromNodeView.MapView),
            fromPort,
            new Tuple<PackedScene, MapView>(toNodeView.MapPrefab, toNodeView.MapView),
            toPort
        );

        if (!couldConnect)
        {
            return;
        }
        
        _graphEdit.ConnectNode(fromNode, fromPort, toNode, toPort);
    }
}