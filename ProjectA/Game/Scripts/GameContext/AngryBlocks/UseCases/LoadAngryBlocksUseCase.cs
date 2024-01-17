using Game.GameContext.AngryBlocks.Views;
using Game.GameContext.Entities.Services;
using GUtils.Pooling.Objects;
using GUtilsGodot.Extensions;

namespace Game.GameContext.AngryBlocks.UseCases;

public sealed class LoadAngryBlocksUseCase : IReturnablePooledObject
{
    IGameEntitiesService? _gameEntitiesService;
    WhenAngryBlockCollidedUseCase? _whenAngryBlockCollidedUseCase;
    RefreshAngryBlockActiveCollidersUseCase? _refreshAngryBlockActiveCollidersUseCase;

    public void Init(
        IGameEntitiesService gameEntitiesService, 
        WhenAngryBlockCollidedUseCase whenAngryBlockCollidedUseCase, 
        RefreshAngryBlockActiveCollidersUseCase refreshAngryBlockActiveCollidersUseCase
        )
    {
        _gameEntitiesService = gameEntitiesService;
        _whenAngryBlockCollidedUseCase = whenAngryBlockCollidedUseCase;
        _refreshAngryBlockActiveCollidersUseCase = refreshAngryBlockActiveCollidersUseCase;
    }
    
    public void PooledObjectReturned()
    {
        _gameEntitiesService = null;
        _whenAngryBlockCollidedUseCase = null;
        _refreshAngryBlockActiveCollidersUseCase = null;
    }

    public void Execute()
    {
        _gameEntitiesService!.ForEachEntity<AngryBlockView>(Load);
    }

    void Load(AngryBlockView angryBlockView)
    {
        angryBlockView.LeftWallDetectorArea2d!.ConnectBodyEntered(_ => _whenAngryBlockCollidedUseCase!.Execute(angryBlockView));
        angryBlockView.RightWallDetectorArea2d!.ConnectBodyEntered(_ => _whenAngryBlockCollidedUseCase!.Execute(angryBlockView));
        angryBlockView.TopWallDetectorArea2d!.ConnectBodyEntered(_ => _whenAngryBlockCollidedUseCase!.Execute(angryBlockView));
        angryBlockView.BottomWallDetectorArea2d!.ConnectBodyEntered(_ => _whenAngryBlockCollidedUseCase!.Execute(angryBlockView));
        
        _refreshAngryBlockActiveCollidersUseCase!.Execute(angryBlockView);
    }
}