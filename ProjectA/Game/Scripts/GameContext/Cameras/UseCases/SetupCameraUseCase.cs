using Game.GameContext.Cameras.Configurations;
using Game.GameContext.Cameras.Datas;
using GUtils.Randomization.Generators;
using GUtilsGodot.Cameras.Behaviours;
using GUtilsGodot.Cameras.PositionProcessors;
using GUtilsGodot.Cameras.Services;
using GUtilsGodot.Extensions;

namespace Game.GameContext.Cameras.UseCases;

public sealed class SetupCameraUseCase
{
    readonly IRandomGenerator _randomGenerator;
    readonly ICameras2dService _cameras2dService;
    readonly CameraBehavioursData _cameraBehavioursData;
    readonly GameCamerasConfiguration _gameCamerasConfiguration;

    public SetupCameraUseCase(
        IRandomGenerator randomGenerator,
        ICameras2dService cameras2dService, 
        CameraBehavioursData cameraBehavioursData, 
        GameCamerasConfiguration gameCamerasConfiguration
        )
    {
        _randomGenerator = randomGenerator;
        _cameras2dService = cameras2dService;
        _cameraBehavioursData = cameraBehavioursData;
        _gameCamerasConfiguration = gameCamerasConfiguration;
    }

    public void Execute()
    {
        _cameras2dService.ClearBehaviours();
        
        FollowZoomCamera2dBehaviour followZoomCamera2dBehaviour = new();
        _cameras2dService.AddBehaviour(followZoomCamera2dBehaviour);
        followZoomCamera2dBehaviour.SetVelocity(_gameCamerasConfiguration.FollowZoomSpeed);
        
        FollowTargetCamera2dBehaviour followTargetCamera2dBehaviour = new();
        _cameras2dService.AddBehaviour(followTargetCamera2dBehaviour);
        followTargetCamera2dBehaviour.SetVelocity(_gameCamerasConfiguration.FollowTargetSpeed);
        
        BoundsPosition2dProcessor boundsPosition2dProcessor = new();
        followTargetCamera2dBehaviour.AddPositionProcessors(boundsPosition2dProcessor);
        _cameraBehavioursData.BoundsProcessor = boundsPosition2dProcessor;
        
        BoundsConfinementCamera2dBehaviour boundsConfinementCamera2dBehaviour = new();
        _cameras2dService.AddBehaviour(boundsConfinementCamera2dBehaviour);
        _cameraBehavioursData.BoundsConfinementBehaviour = boundsConfinementCamera2dBehaviour;
        
        ShakeCamera2dBehaviour shakeCamera2dBehaviour = new(_randomGenerator);
        _cameras2dService.AddBehaviour(shakeCamera2dBehaviour);
        shakeCamera2dBehaviour.SetAttenuationStrenght(_gameCamerasConfiguration.ShakeAttenuation);
        _cameraBehavioursData.ShakeBehaviour = shakeCamera2dBehaviour;
    }
}