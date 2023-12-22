using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Cinematics.Contexts;
using Game.GameContext.Cinematics.Datas;
using Game.GameContext.Cinematics.Interfaces;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using Godot;
using GUtils.Tasks.Runners;
using TaskExtensions = GUtilsGodot.Extensions.TaskExtensions;

namespace Game.GameContext.Cinematics.UseCases;

public sealed class PlayCinematicUseCase
{
    readonly PlayerViewData _playerViewData;
    readonly CinematicsMethods _cinematicsMethods;
    readonly IAsyncTaskRunner _asyncTaskRunner;
    readonly AwaitUntilPlayerIsOnTheGroundUseCase _awaitUntilPlayerIsOnTheGroundUseCase;

    public PlayCinematicUseCase(
        PlayerViewData playerViewData, 
        CinematicsMethods cinematicsMethods,
        IAsyncTaskRunner asyncTaskRunner,
        AwaitUntilPlayerIsOnTheGroundUseCase awaitUntilPlayerIsOnTheGroundUseCase
        )
    {
        _playerViewData = playerViewData;
        _cinematicsMethods = cinematicsMethods;
        _asyncTaskRunner = asyncTaskRunner;
        _awaitUntilPlayerIsOnTheGroundUseCase = awaitUntilPlayerIsOnTheGroundUseCase;
    }

    public void Execute(ICinematic cinematic)
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);
        
        if(!hasPlayer)
        {
            return;
        }

        CinematicsContext cinematicsContext = new(
            playerView,
            _cinematicsMethods
        );

        async Task Play(CancellationToken cancellationToken)
        {
            await TaskExtensions.GodotYield();
            
            if(cancellationToken.IsCancellationRequested) return;
            
            playerView.CanMove = false;
            playerView.InteractionsDetector!.ProcessMode = Node.ProcessModeEnum.Disabled;
            
            if(cancellationToken.IsCancellationRequested) return;
            
            await cinematic.PlayCinematic(cinematicsContext, cancellationToken);
            
            if(cancellationToken.IsCancellationRequested) return;
            
            playerView.CanMove = true;
            playerView.InteractionsDetector!.ProcessMode = Node.ProcessModeEnum.Inherit;
        }

        _asyncTaskRunner.Run(Play);
    }
}