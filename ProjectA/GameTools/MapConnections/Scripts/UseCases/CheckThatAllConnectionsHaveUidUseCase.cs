using System;
using Game.GameContext.Connections.Views;
using GameTools.MapConnections.Scripts.Datas;
using GameTools.MapConnections.Scripts.Views;
using Godot;
using GUtilsGodot.Extensions;

namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class CheckThatAllConnectionsHaveUidUseCase
{
    readonly MapNodesData _mapNodesData;

    public CheckThatAllConnectionsHaveUidUseCase(
        MapNodesData mapNodesData
        )
    {
        _mapNodesData = mapNodesData;
    }

    public void Execute()
    {
        foreach (MapConnectionNodeView mapConnectionNodeView in _mapNodesData.MapConnectionNodeViews)
        {
            bool needsToSave = false;
            
            foreach (ConnectionView connectionView in mapConnectionNodeView.MapView.ConnectionViews!)
            {
                bool hasUid = !string.IsNullOrEmpty(connectionView.Uid);

                if (!hasUid)
                {
                    connectionView.Uid = Guid.NewGuid().ToString();
                    needsToSave = true;
                }
            }

            if (needsToSave)
            {
                mapConnectionNodeView.MapPrefab.Pack(mapConnectionNodeView.MapView);
                mapConnectionNodeView.MapPrefab.Save();
                
                GD.Print("Connections uid created");
            }
        }
    }
}