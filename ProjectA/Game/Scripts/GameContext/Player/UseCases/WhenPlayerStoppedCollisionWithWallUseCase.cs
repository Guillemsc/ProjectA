using Game.GameContext.Player.Datas;
using Game.GameContext.Player.Views;

namespace Game.GameContext.Player.UseCases;

public sealed class WhenPlayerStoppedCollisionWithWallUseCase
{
    readonly PlayerViewData _playerViewData;

    public WhenPlayerStoppedCollisionWithWallUseCase(PlayerViewData playerViewData)
    {
        _playerViewData = playerViewData;
    }

    public void Execute()
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }

        playerView.OnWall = false;
    }
}