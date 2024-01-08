using Game.GameContext.Cameras.Configurations;
using Game.GameContext.Cameras.Datas;
using GUtilsGodot.Cameras.Behaviours;

namespace Game.GameContext.Cameras.UseCases;

public sealed class ShakeCameraUseCase
{
    readonly CameraBehavioursData _cameraBehavioursData;
    readonly GameCamerasConfiguration _gameCamerasConfiguration;

    public ShakeCameraUseCase(
        CameraBehavioursData cameraBehavioursData, 
        GameCamerasConfiguration gameCamerasConfiguration
        )
    {
        _cameraBehavioursData = cameraBehavioursData;
        _gameCamerasConfiguration = gameCamerasConfiguration;
    }

    public void Execute()
    {
        bool hasBehaviour = _cameraBehavioursData.ShakeBehaviour.TryGet(
            out ShakeCamera2dBehaviour shakeCamera2dBehaviour
        );

        if (!hasBehaviour)
        {
            return;
        }
        
        shakeCamera2dBehaviour.ApplyImpulse(_gameCamerasConfiguration.ShakeStrenght);
    }
}