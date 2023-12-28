using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Cinematics.Contexts;
using Game.GameContext.Cinematics.Datas;
using Game.GameContext.Cinematics.Interfaces;
using Game.GameContext.General.Configurations;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using Godot;
using GUtils.Tasks.Runners;
using TaskExtensions = GUtilsGodot.Extensions.TaskExtensions;

namespace Game.GameContext.Cinematics.UseCases;

public sealed class PlayCinematicUseCase
{
    readonly GameConfiguration _gameConfiguration;
    readonly PlayerViewData _playerViewData;
    readonly CinematicsMethods _cinematicsMethods;
    readonly IAsyncTaskRunner _asyncTaskRunner;

    public PlayCinematicUseCase(
        GameConfiguration gameConfiguration,
        PlayerViewData playerViewData, 
        CinematicsMethods cinematicsMethods,
        IAsyncTaskRunner asyncTaskRunner
        )
    {
        _gameConfiguration = gameConfiguration;
        _playerViewData = playerViewData;
        _cinematicsMethods = cinematicsMethods;
        _asyncTaskRunner = asyncTaskRunner;
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
            _gameConfiguration,
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