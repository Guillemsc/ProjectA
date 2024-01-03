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
    readonly CurrentCinematicData _currentCinematicData;
    readonly GameConfiguration _gameConfiguration;
    readonly CinematicsServices _cinematicsServices;
    readonly PlayerViewData _playerViewData;
    readonly CinematicsMethods _cinematicsMethods;
    readonly IAsyncTaskRunner _asyncTaskRunner;

    public PlayCinematicUseCase(
        CurrentCinematicData currentCinematicData,
        GameConfiguration gameConfiguration,
        CinematicsServices cinematicsServices,
        PlayerViewData playerViewData, 
        CinematicsMethods cinematicsMethods,
        IAsyncTaskRunner asyncTaskRunner
        )
    {
        _currentCinematicData = currentCinematicData;
        _gameConfiguration = gameConfiguration;
        _cinematicsServices = cinematicsServices;
        _playerViewData = playerViewData;
        _cinematicsMethods = cinematicsMethods;
        _asyncTaskRunner = asyncTaskRunner;
    }

    public void Execute(ICinematic cinematic)
    {
        if (_currentCinematicData.CurrentCinematicSkipTokenSource != null)
        {
            return;
        }
        
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);
        
        if(!hasPlayer)
        {
            return;
        }

        CinematicContext cinematicContext = new(
            playerView,
            _gameConfiguration,
            _cinematicsMethods,
            _cinematicsServices
        );

        async Task Play(CancellationToken cancellationToken)
        {
            await TaskExtensions.GodotYield();
            
            if(cancellationToken.IsCancellationRequested) return;
            
            playerView.CanMove = false;
            playerView.InteractionsDetector!.ProcessMode = Node.ProcessModeEnum.Disabled;
            
            if(cancellationToken.IsCancellationRequested) return;

            _currentCinematicData.CurrentCinematicSkipTokenSource = new CancellationTokenSource();

            await cinematic.Play(
                cinematicContext,
                _currentCinematicData.CurrentCinematicSkipTokenSource.Token,
                cancellationToken
            );
            
            _currentCinematicData.CurrentCinematicSkipTokenSource.Dispose();
            _currentCinematicData.CurrentCinematicSkipTokenSource = null;
            
            if(cancellationToken.IsCancellationRequested) return;
            
            playerView.CanMove = true;
            playerView.InteractionsDetector!.ProcessMode = Node.ProcessModeEnum.Inherit;
        }

        _asyncTaskRunner.Run(Play);
    }
}