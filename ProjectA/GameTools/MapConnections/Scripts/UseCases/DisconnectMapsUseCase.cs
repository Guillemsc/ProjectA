using System;
using Game.GameContext.Connections.Enums;
using Game.GameContext.Connections.Views;
using Game.GameContext.Maps.Views;
using Godot;
using GUtils.Optionals;
using GUtilsGodot.Extensions;

namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class DisconnectMapsUseCase
{
    readonly GetMapConnectionForConnectionTypeWithIndexUseCase _getMapConnectionForConnectionTypeWithIndexUseCase;

    public DisconnectMapsUseCase(
        GetMapConnectionForConnectionTypeWithIndexUseCase getMapConnectionForConnectionTypeWithIndexUseCase
    )
    {
        _getMapConnectionForConnectionTypeWithIndexUseCase = getMapConnectionForConnectionTypeWithIndexUseCase;
    }

    public bool Execute(
        Tuple<PackedScene, MapView> from, int fromConnectionIndex, 
        Tuple<PackedScene, MapView> to, int toConnectionIndex
    )
    {
        Optional<ConnectionView> optionalFromConnection = _getMapConnectionForConnectionTypeWithIndexUseCase.Execute(
            from.Item2,
            ConnectionType.Exit,
            fromConnectionIndex
        );
        
        bool fromConnectionFound = optionalFromConnection!.TryGet(
            out ConnectionView fromConnectionView
        );

        if (!fromConnectionFound)
        {
            return false;
        }
        
        Optional<ConnectionView> optionalToConnection = _getMapConnectionForConnectionTypeWithIndexUseCase.Execute(
            to.Item2,
            ConnectionType.Enter,
            toConnectionIndex
        );
        
        bool toConnectionFound = optionalToConnection!.TryGet(
            out ConnectionView toConnectionView
        );

        if (!toConnectionFound)
        {
            return false;
        }

        fromConnectionView.Map = string.Empty;
        fromConnectionView.SpawnId = string.Empty;
        
        toConnectionView.Map = string.Empty;
        toConnectionView.SpawnId = string.Empty;
        
        from.Item1.Pack(from.Item2);
        from.Item1.Save();
        
        to.Item1.Pack(to.Item2);
        to.Item1.Save();
        
        return true;
    }
}