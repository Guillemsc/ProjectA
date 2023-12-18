using Game.GameContext.Players.Views;
using Godot;
using GUtils.Extensions;
using GUtils.Time.Extensions;
using GUtils.Time.Timers;

namespace Game.GameContext.Players.UseCases;

public sealed class StorePlayerPreviousPositionUseCase
{
    readonly ITimer _timer = new StopwatchTimer();
    
    public void Execute(PlayerView playerView)
    {
        if (!_timer.HasReachedOrNotStarted(0.2f.ToSeconds()))
        {
            return;
        }
        
        _timer.Restart();
        
        Vector2 position = playerView.GlobalPosition;
        
        playerView.PreviousPositions.Add(position);

        if (playerView.PreviousPositions.Count < 2)
        {
            return;
        }
        
        playerView.PreviousPositions.RemoveFirst();
    }
}