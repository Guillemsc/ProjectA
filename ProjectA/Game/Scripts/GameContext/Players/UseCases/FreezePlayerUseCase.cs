using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using Godot;

namespace Game.GameContext.Players.UseCases;

public sealed class FreezePlayerUseCase
{
    readonly PlayerViewData _playerViewData;

    public FreezePlayerUseCase(PlayerViewData playerViewData)
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
        
        playerView.AnimationPlayer!.ProcessMode = Node.ProcessModeEnum.Disabled;
        playerView.CanUpdateMovement = false;
        playerView.CanMove = false;
    }
}