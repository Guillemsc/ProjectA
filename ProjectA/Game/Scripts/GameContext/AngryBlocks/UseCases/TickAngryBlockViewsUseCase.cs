using Game.GameContext.AngryBlocks.Views;
using Game.GameContext.Entities.Services;
using GUtils.Executables;
using GUtils.Pooling.Objects;

namespace Game.GameContext.AngryBlocks.UseCases;

public sealed class TickAngryBlockViewsUseCase : IReturnablePooledObject, IExecutable
{
    IGameEntitiesService? _gameEntitiesService;
    TickAngryBlockViewUseCase? _tickAngryBlockViewUseCase;

    public void Init(
        IGameEntitiesService gameEntitiesService, 
        TickAngryBlockViewUseCase tickAngryBlockViewUseCase
        )
    {
        _gameEntitiesService = gameEntitiesService;
        _tickAngryBlockViewUseCase = tickAngryBlockViewUseCase;
    }

    public void PooledObjectReturned()
    {
        _gameEntitiesService = null;
        _tickAngryBlockViewUseCase = null;
    }
    
    public void Execute()
    {
        _gameEntitiesService!.ForEachEntity<AngryBlockView>(_tickAngryBlockViewUseCase!.Execute);
    }
}