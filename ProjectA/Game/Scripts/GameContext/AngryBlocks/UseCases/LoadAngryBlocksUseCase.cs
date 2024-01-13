using Game.GameContext.AngryBlocks.Views;
using Game.GameContext.Entities.Services;
using Godot;
using GUtilsGodot.Extensions;

namespace Game.GameContext.AngryBlocks.UseCases;

public sealed class LoadAngryBlocksUseCase
{
    readonly IGameEntitiesService _gameEntitiesService;
    readonly WhenAngryBlockCollidedUseCase _whenAngryBlockCollidedUseCase;
    readonly RefreshAngryBlockActiveCollidersUseCase _refreshAngryBlockActiveCollidersUseCase;

    public LoadAngryBlocksUseCase(
        IGameEntitiesService gameEntitiesService, 
        WhenAngryBlockCollidedUseCase whenAngryBlockCollidedUseCase, 
        RefreshAngryBlockActiveCollidersUseCase refreshAngryBlockActiveCollidersUseCase
        )
    {
        _gameEntitiesService = gameEntitiesService;
        _whenAngryBlockCollidedUseCase = whenAngryBlockCollidedUseCase;
        _refreshAngryBlockActiveCollidersUseCase = refreshAngryBlockActiveCollidersUseCase;
    }

    public void Execute()
    {
        _gameEntitiesService.ForEachEntity<AngryBlockView>(Load);
    }

    void Load(AngryBlockView angryBlockView)
    {
        angryBlockView.LeftWallDetectorArea2d!.ConnectBodyEntered(_ => _whenAngryBlockCollidedUseCase.Execute(angryBlockView));
        angryBlockView.RightWallDetectorArea2d!.ConnectBodyEntered(_ => _whenAngryBlockCollidedUseCase.Execute(angryBlockView));
        angryBlockView.TopWallDetectorArea2d!.ConnectBodyEntered(_ => _whenAngryBlockCollidedUseCase.Execute(angryBlockView));
        angryBlockView.BottomWallDetectorArea2d!.ConnectBodyEntered(_ => _whenAngryBlockCollidedUseCase.Execute(angryBlockView));
        
        _refreshAngryBlockActiveCollidersUseCase.Execute(angryBlockView);
    }
}