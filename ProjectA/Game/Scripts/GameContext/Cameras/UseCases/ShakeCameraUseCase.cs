using Game.GameContext.Cameras.Configurations;
using Game.GameContext.Cameras.Datas;
using Game.GameContext.Cameras.Enums;
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

    public void Execute(ShakeCameraStrenght shakeCameraStrenght)
    {
        bool hasBehaviour = _cameraBehavioursData.ShakeBehaviour.TryGet(
            out ShakeCamera2dBehaviour shakeCamera2dBehaviour
        );

        if (!hasBehaviour)
        {
            return;
        }

        float strenght = shakeCameraStrenght switch
        {
            ShakeCameraStrenght.Low => _gameCamerasConfiguration.LowShakeStrenght,
            ShakeCameraStrenght.Middle => _gameCamerasConfiguration.MiediumShakeStrenght,
            ShakeCameraStrenght.Strong => _gameCamerasConfiguration.StrongShakeStrenght,
            _ => _gameCamerasConfiguration.LowShakeStrenght
        };

        shakeCamera2dBehaviour.ApplyImpulse(strenght);
    }
}