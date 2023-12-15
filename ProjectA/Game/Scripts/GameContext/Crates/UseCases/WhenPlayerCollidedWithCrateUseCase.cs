using Game.GameContext.Crates.Views;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using Godot;

namespace Game.GameContext.Crates.UseCases;

public sealed class WhenPlayerCollidedWithCrateUseCase
{
    readonly PlayerViewData _playerViewData;

    public WhenPlayerCollidedWithCrateUseCase(PlayerViewData playerViewData)
    {
        _playerViewData = playerViewData;
    }

    public void Execute(CrateView crateView, KinematicCollision2D kinematicCollision2D)
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }
        
        Vector2 collisionNormal = kinematicCollision2D.GetNormal();

        bool onTop = collisionNormal is { X: 0f, Y: < 0f };
        bool onBottom = collisionNormal is { X: 0f, Y: > 0f };

        if (onTop)
        {
            Vector2 newVelocity = playerView.Velocity;
            newVelocity.Y = -crateView.BounceStrenght;
            playerView.Velocity = newVelocity;
        }
        else if(onBottom)
        {
            GD.Print("Bottom");
        }
    }
}