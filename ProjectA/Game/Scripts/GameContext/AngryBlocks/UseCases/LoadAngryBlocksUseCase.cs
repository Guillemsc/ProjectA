using Game.GameContext.AngryBlocks.Views;
using Game.GameContext.Entities.Services;
using Godot;
using GUtilsGodot.Extensions;

namespace Game.GameContext.AngryBlocks.UseCases;

public sealed class LoadAngryBlocksUseCase
{
    readonly IGameEntitiesService _gameEntitiesService;
    readonly WhenAngryBlockCollidedUseCase _whenAngryBlockCollidedUseCase;

    public LoadAngryBlocksUseCase(
        IGameEntitiesService gameEntitiesService, 
        WhenAngryBlockCollidedUseCase whenAngryBlockCollidedUseCase
        )
    {
        _gameEntitiesService = gameEntitiesService;
        _whenAngryBlockCollidedUseCase = whenAngryBlockCollidedUseCase;
    }

    public void Execute()
    {
        _gameEntitiesService.ForEachEntity<AngryBlockView>(Load);
    }

    void Load(AngryBlockView angryBlockView)
    {
        angryBlockView.WallDetectorArea2d!.ConnectBodyEntered(_ => _whenAngryBlockCollidedUseCase.Execute(angryBlockView));
    }
}