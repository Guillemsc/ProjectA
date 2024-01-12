using Game.ServicesContext.Time.Services;
using GUtils.Saws.Views;

namespace Game.GameContext.Saws.UseCases;

public sealed class TickSawMovementUseCase
{
    readonly IGameTimesService _gameTimesService;

    public TickSawMovementUseCase(
        IGameTimesService gameTimesService
    )
    {
        _gameTimesService = gameTimesService;
    }
    
    public void Execute(SawView sawView)
    {
        sawView.AnimationGraphPlayer!.On = true;
        
        float delta = _gameTimesService.PhysicsTimeContext.DeltaTime;

        sawView.Loop = sawView.MovementConfiguration!.Loops;

        if (!sawView.MovementConfiguration!.Loops)
        {
            bool changeDirection = sawView.ProgressRatio >= 1 || sawView.ProgressRatio <= 0;

            if (changeDirection)
            {
                sawView.CurrentDirection *= -1;
            }
        }
        
        float speed = sawView.MovementConfiguration!.Speed * sawView.CurrentDirection;
        float deltaVelocity = speed * delta;
        
        sawView.Progress += deltaVelocity;
    }
}