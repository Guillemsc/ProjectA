using Game.GameContext.Entities.Services;
using GUtils.Executables;
using GUtils.Saws.Views;

namespace Game.GameContext.Saws.UseCases;

public sealed class TickSawViewsUseCase : IExecutable
{ 
    readonly IGameEntitiesService _gameEntitiesService;
    readonly TickSawViewUseCase _tickSawViewUseCase;

    public TickSawViewsUseCase(
        IGameEntitiesService gameEntitiesService, 
        TickSawViewUseCase tickSawViewUseCase
        )
    {
        _gameEntitiesService = gameEntitiesService;
        _tickSawViewUseCase = tickSawViewUseCase;
    }

    public void Execute()
    {
        _gameEntitiesService.ForEachEntity<SawView>(_tickSawViewUseCase.Execute);
    }
}