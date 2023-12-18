using Game.GameContext.Connections.Datas;
using Game.GameContext.Connections.Views;
using GUtils.Optionals;

namespace Game.GameContext.Connections.UseCases;

public sealed class RefreshCurrentPlayerConnectionUseCase
{
    readonly ConnectionsData _connectionsData;
    readonly GetCurrentPlayerConnectionUseCase _getCurrentPlayerConnectionUseCase;
    readonly WhenCurrentPlayerConnectionChangedUseCase _whenCurrentPlayerConnectionChangedUseCase;

    public RefreshCurrentPlayerConnectionUseCase(
        ConnectionsData connectionsData, 
        GetCurrentPlayerConnectionUseCase getCurrentPlayerConnectionUseCase,
        WhenCurrentPlayerConnectionChangedUseCase whenCurrentPlayerConnectionChangedUseCase
        )
    {
        _connectionsData = connectionsData;
        _getCurrentPlayerConnectionUseCase = getCurrentPlayerConnectionUseCase;
        _whenCurrentPlayerConnectionChangedUseCase = whenCurrentPlayerConnectionChangedUseCase;
    }

    public void Execute()
    {
        Optional<ConnectionView> optionalConnection = _getCurrentPlayerConnectionUseCase.Execute();

        bool hasConnection = optionalConnection.TryGet(out ConnectionView connection);
        
        if (!hasConnection)
        {
            _connectionsData.CurrentConnection = Optional<ConnectionView>.None;
            return;
        }

        bool changed = true;

        bool hasCurrentConnection = _connectionsData.CurrentConnection.TryGet(out ConnectionView currentConnection);

        if (hasCurrentConnection)
        {
            if (currentConnection == connection)
            {
                changed = false;
            }
        }

        if (!changed)
        {
            return;
        }

        _connectionsData.CurrentConnection = connection;
        _whenCurrentPlayerConnectionChangedUseCase.Execute(connection);
    }
}