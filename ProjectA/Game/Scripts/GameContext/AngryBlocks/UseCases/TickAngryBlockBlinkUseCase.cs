using System;
using Game.GameContext.AngryBlocks.Configurations;
using Game.GameContext.AngryBlocks.Enums;
using Game.GameContext.AngryBlocks.Views;
using Game.ServicesContext.Time.Services;
using GUtils.Extensions;
using GUtils.Randomization.Generators;

namespace Game.GameContext.AngryBlocks.UseCases;

public sealed class TickAngryBlockBlinkUseCase
{
    readonly IGameTimesService _gameTimesService;
    readonly IRandomGenerator _randomGenerator;
    readonly GameAngryBlocksConfiguration _gameAngryBlocksConfiguration;

    public TickAngryBlockBlinkUseCase(
        IGameTimesService gameTimesService, 
        IRandomGenerator randomGenerator,
        GameAngryBlocksConfiguration gameAngryBlocksConfiguration
        )
    {
        _gameTimesService = gameTimesService;
        _randomGenerator = randomGenerator;
        _gameAngryBlocksConfiguration = gameAngryBlocksConfiguration;
    }

    public void Execute(AngryBlockView angryBlockView)
    {
        if (angryBlockView.TimeLeftUntilNextBlink <= TimeSpan.Zero)
        {
            float randomTime = _randomGenerator.NewFloat(
                _gameAngryBlocksConfiguration.MinBlinkTimeSeconds,
                _gameAngryBlocksConfiguration.MaxBlinkTimeSeconds
            );
            
            angryBlockView.TimeLeftUntilNextBlink = randomTime.ToSeconds();
            
            angryBlockView.AnimationGraphPlayer!.NeedsToPlayBlink = true;
        }

        bool isIdle = angryBlockView.AnimationGraphPlayer!.AnimatedSprite2D!.Animation == AngryBlockAnimationState.Idle.ToString();

        if (!isIdle)
        {
            return;
        }

        angryBlockView.TimeLeftUntilNextBlink -= _gameTimesService.TimeContext.DeltaTime.ToSeconds();
    }
}