using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;

namespace Game.GameContext.Players.UseCases;

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