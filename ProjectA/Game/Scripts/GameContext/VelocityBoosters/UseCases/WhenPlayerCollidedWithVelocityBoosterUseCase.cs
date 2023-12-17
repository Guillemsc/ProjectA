using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using Game.GameContext.VelocityBoosters.Views;
using Godot;
using GUtils.Extensions;
using GUtilsGodot.Extensions;

namespace Game.GameContext.VelocityBoosters.UseCases;

public sealed class WhenPlayerCollidedWithVelocityBoosterUseCase
{
    readonly PlayerViewData _playerViewData;

    public WhenPlayerCollidedWithVelocityBoosterUseCase(
        PlayerViewData playerViewData
    )
    {
        _playerViewData = playerViewData;
    }
    
    public void Execute(VelocityBoosterView velocityBoosterView)
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);

        if (!hasPlayer)
        {
            return;
        }
        
        ActionExtensions.CallDeferred(() => velocityBoosterView.Area2D!.Monitorable = false);
        
        Vector2 direction = GUtilsGodot.Extensions.MathExtensions.GetDirectionFromAngle(
            velocityBoosterView.GlobalRotationDegrees - 90
        );
        
        velocityBoosterView.AnimationPlayer!.NeedsToPlayCollected = true;
        
        Vector2 newVelocity = playerView.Velocity;
        newVelocity = direction * velocityBoosterView.BoostStrenght;
        playerView.Velocity = newVelocity;
    }
}