using Game.GameContext.AngryBlocks.Views;
using Game.GameContext.Entities.Services;
using GUtils.Executables;

namespace Game.GameContext.AngryBlocks.UseCases;

public sealed class TickAngryBlockViewsUseCase : IExecutable
{
    readonly IGameEntitiesService _gameEntitiesService;
    readonly TickAngryBlockViewUseCase _tickAngryBlockViewUseCase;

    public TickAngryBlockViewsUseCase(
        IGameEntitiesService gameEntitiesService, 
        TickAngryBlockViewUseCase tickAngryBlockViewUseCase
        )
    {
        _gameEntitiesService = gameEntitiesService;
        _tickAngryBlockViewUseCase = tickAngryBlockViewUseCase;
    }

    public void Execute()
    {
        _gameEntitiesService.ForEachEntity<AngryBlockView>(_tickAngryBlockViewUseCase.Execute);
    }
}