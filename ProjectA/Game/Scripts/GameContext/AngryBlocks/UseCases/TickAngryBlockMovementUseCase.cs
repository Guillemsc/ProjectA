using Game.GameContext.AngryBlocks.Configurations;
using Game.GameContext.AngryBlocks.Views;
using Game.ServicesContext.Time.Services;
using Godot;
using GUtils.Directions;
using GUtils.Extensions;
using GUtilsGodot.Directions.Extensions;

namespace Game.GameContext.AngryBlocks.UseCases;

public sealed class TickAngryBlockMovementUseCase
{
    readonly IGameTimesService _gameTimesService;
    readonly GameAngryBlocksConfiguration _gameAngryBlocksConfiguration;

    public TickAngryBlockMovementUseCase(
        IGameTimesService gameTimesService, 
        GameAngryBlocksConfiguration gameAngryBlocksConfiguration
        )
    {
        _gameTimesService = gameTimesService;
        _gameAngryBlocksConfiguration = gameAngryBlocksConfiguration;
    }

    public void Execute(AngryBlockView angryBlockView)
    {
        bool couldGetCurrentDirection = angryBlockView.MovementSequence!.TryGet(
            angryBlockView.CurrentMovementSequenceIndex,
            out CardinalDirection cardinalDirection
        );

        if (!couldGetCurrentDirection)
        {
            return;
        }

        float delta = _gameTimesService.PhysicsTimeContext.DeltaTime;

        float deltaAcceleration = angryBlockView.MovementConfiguration!.Acceleration * delta;

        angryBlockView.CurrentMovementSpeed = Mathf.Min(
            angryBlockView.CurrentMovementSpeed + deltaAcceleration,
            angryBlockView.MovementConfiguration.MaxSpeed
        );

        Vector2 direction = cardinalDirection.ToDirectionVector();

        Vector2 speedVector = direction * angryBlockView.CurrentMovementSpeed * delta;

        angryBlockView.Position += speedVector;
    }
}