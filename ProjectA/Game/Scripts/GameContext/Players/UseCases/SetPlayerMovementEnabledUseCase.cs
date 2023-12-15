using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;

namespace Game.GameContext.Players.UseCases;

public sealed class SetPlayerMovementEnabledUseCase
{
    readonly PlayerViewData _playerViewData;

    public SetPlayerMovementEnabledUseCase(
        PlayerViewData playerViewData
        )
    {
        _playerViewData = playerViewData;
    }

    public void Execute(bool enabled)
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }

        playerView.CanMove = enabled;
    }
}