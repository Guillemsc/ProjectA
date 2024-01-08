using Game.GameContext.Crates.Views;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using Godot;

namespace Game.GameContext.Crates.UseCases;

public sealed class WhenPlayerCollidedWithCrateUseCase
{
    readonly PlayerViewData _playerViewData;
    readonly SpawnFruitCollectablesWhenCrateHitUseCase _spawnFruitCollectablesWhenCrateHitUseCase;
    
    public WhenPlayerCollidedWithCrateUseCase(
        PlayerViewData playerViewData, 
        SpawnFruitCollectablesWhenCrateHitUseCase spawnFruitCollectablesWhenCrateHitUseCase
        )
    {
        _playerViewData = playerViewData;
        _spawnFruitCollectablesWhenCrateHitUseCase = spawnFruitCollectablesWhenCrateHitUseCase;
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

        bool isHit = false;
        
        if (onTop)
        {
            playerView.MovementVelocity.Y = -crateView.BounceStrenght;

            crateView.AnimationPlayer!.NeedsToPlayHit = true;
            isHit = true;
        }
        else if(onBottom)
        {
            crateView.AnimationPlayer!.NeedsToPlayHit = true;
            isHit = true;
        }

        if (!isHit)
        {
            return;
        }

        crateView.HitsCount += 1;

        bool shouldBeDestroyed = crateView.HitsCount >= crateView.HitsToDestroy;

        if (!shouldBeDestroyed)
        {
            _spawnFruitCollectablesWhenCrateHitUseCase.Execute(crateView, crateView.FruitsPerHit);
        }
        else
        {
            crateView.AnimationPlayer!.Broken = true;
            crateView.AnimationPlayer.SpawnContents += () =>
            {
                _spawnFruitCollectablesWhenCrateHitUseCase.Execute(crateView, crateView.FruitsWhenDestroyed);
            }; 
        }
    }
}