using Game.GameContext.Connections.Enums;
using Game.GameContext.Connections.Views;
using Game.GameContext.Maps.Views;
using GUtils.Optionals;

namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class GetMapConnectionForConnectionTypeWithIndexUseCase
{
    public Optional<ConnectionView> Execute(MapView mapView, ConnectionType connectionType, int index)
    {
        int currentIndex = 0;
        foreach (ConnectionView connectionView in mapView.ConnectionViews!)
        {
            if (connectionType != connectionView.ConnectionType)
            {
                continue;
            }

            if (currentIndex == index)
            {
                return connectionView;
            }

            ++currentIndex;
        }

        return Optional<ConnectionView>.None;
    }
}