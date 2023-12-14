using Game.GameContext.Player.Datas;
using Game.GameContext.Player.Views;
using GUtils.Locations.Enums;

namespace Game.GameContext.Player.UseCases;

public sealed class WhenPlayerStartedCollisionWithWallUseCase
{
    readonly PlayerViewData _playerViewData;

    public WhenPlayerStartedCollisionWithWallUseCase(PlayerViewData playerViewData)
    {
        _playerViewData = playerViewData;
    }

    public void Execute(HorizontalLocation location)
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }

        playerView.OnWall = true;
        playerView.OnWallLocation = location;
    }
}