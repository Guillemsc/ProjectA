using System;
using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.General.ApplicationContexts;
using Game.GameContext.General.Configurations;
using Game.GameContext.PlayerKillers.Datas;
using Game.GameContext.Players.UseCases;
using GUtils.Loading.Extensions;
using GUtils.Loading.Services;
using GUtils.Tasks.Runners;

namespace Game.GameContext.PlayerKillers.UseCases;

public sealed class KillPlayerAndReloadUseCase
{
    readonly GameApplicationContextConfiguration _gameApplicationContextConfiguration;
    readonly PlayerKillersData _playerKillersData;
    readonly IAsyncTaskRunner _asyncTaskRunner;
    readonly ILoadingService _loadingService;
    readonly KillPlayerUseCase _killPlayerUseCase;

    public KillPlayerAndReloadUseCase(
        GameApplicationContextConfiguration gameApplicationContextConfiguration,
        PlayerKillersData playerKillersData,
        IAsyncTaskRunner asyncTaskRunner,
        ILoadingService loadingService,
        KillPlayerUseCase killPlayerUseCase
        )
    {
        _gameApplicationContextConfiguration = gameApplicationContextConfiguration;
        _playerKillersData = playerKillersData;
        _asyncTaskRunner = asyncTaskRunner;
        _loadingService = loadingService;
        _killPlayerUseCase = killPlayerUseCase;
    }

    public void Execute()
    {
        if (_playerKillersData.ReloadingBecauseOfPlayerKiller)
        {
            return;
        }
        
        _playerKillersData.ReloadingBecauseOfPlayerKiller = true;
        
        async Task Play(CancellationToken cancellationToken)
        {
            await _killPlayerUseCase.Execute(cancellationToken);
            
            GameApplicationContextConfiguration contextConfiguration = new(
                _gameApplicationContextConfiguration.MapToLoad,
                _gameApplicationContextConfiguration.SpawnId,
                true,
                _gameApplicationContextConfiguration.PlayerDirection
            );

            if (_loadingService.IsLoading)
            {
                int i = 0;
            }
            
            _loadingService.New()
                .EnqueueUnloadApplicationContext<GameApplicationContext>()
                .EnqueueLoadAndStartApplicationContext(new GameApplicationContext(contextConfiguration))
                .ExecuteAsync();
        }

        _asyncTaskRunner.Run(Play);
    }
}