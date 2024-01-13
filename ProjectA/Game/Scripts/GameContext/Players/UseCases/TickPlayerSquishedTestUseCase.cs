using Game.GameContext.PlayerKillers.UseCases;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using Godot;
using GUtils.Executables;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Players.UseCases;

public sealed class TickPlayerSquishedTestUseCase : IExecutable
{
    readonly PlayerViewData _playerViewData;
    readonly KillPlayerAndReloadUseCase _killPlayerAndReloadUseCase;

    public TickPlayerSquishedTestUseCase(
        PlayerViewData playerViewData, 
        KillPlayerAndReloadUseCase killPlayerAndReloadUseCase
        )
    {
        _playerViewData = playerViewData;
        _killPlayerAndReloadUseCase = killPlayerAndReloadUseCase;
    }

    public void Execute()
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }
        
        if (!playerView.CanUpdateMovement)
        {
            return;
        }

        bool leftCollision = playerView.LeftSqushedRayCasts.IsAnyColliding();
        bool rightCollision = playerView.RightSqushedRayCasts.IsAnyColliding();
        bool topCollision = playerView.TopSqushedRayCasts.IsAnyColliding();
        bool bottomCollision = playerView.BottomSqushedRayCasts.IsAnyColliding();

        bool shouldKill = leftCollision && rightCollision || topCollision && bottomCollision;

        if (shouldKill)
        {
            _killPlayerAndReloadUseCase.Execute();
        }
    }
}