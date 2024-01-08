using Game.GameContext.Areas.UseCases;
using Game.GameContext.Cameras.Configurations;
using Game.GameContext.Cameras.Datas;
using Game.GameContext.Cameras.UseCases;
using Game.GameContext.Maps.Datas;
using Game.GameContext.Players.Datas;
using GUtils.Di.Builder;
using GUtils.Randomization.Generators;
using GUtilsGodot.Cameras.Services;

namespace Game.GameContext.Cameras.Installers;

public static class GameCamerasInstallers
{
    public static void InstallGameCameras(this IDiContainerBuilder builder)
    {
        builder.Bind<CameraBehavioursData>().FromNew();

        builder.Bind<SetupCameraUseCase>()
            .FromFunction(c => new SetupCameraUseCase(
                c.Resolve<IRandomGenerator>(),
                c.Resolve<ICameras2dService>(),
                c.Resolve<CameraBehavioursData>(),
                c.Resolve<GameCamerasConfiguration>()
            ));

        builder.Bind<DisposeCameraSetupUseCase>()
            .FromFunction(c => new DisposeCameraSetupUseCase(
                c.Resolve<ICameras2dService>()
            ))
            .WhenDispose(o => o.Execute)
            .NonLazy();

        builder.Bind<SetInitialCameraAreaUseCase>()
            .FromFunction(c => new SetInitialCameraAreaUseCase(
                c.Resolve<GetCurrentPlayerAreaUseCase>(),
                c.Resolve<SetCameraAreaUseCase>()
            ));

        builder.Bind<SetCameraMapBoundsUseCase>()
            .FromFunction(c => new SetCameraMapBoundsUseCase(
                c.Resolve<CameraBehavioursData>(),
                c.Resolve<MapViewData>()
            ));
        
        builder.Bind<SetPlayerAsCameraTargetUseCase>()
            .FromFunction(c => new SetPlayerAsCameraTargetUseCase(
                c.Resolve<ICameras2dService>(),
                c.Resolve<PlayerViewData>()
            ));

        builder.Bind<SetCameraAreaUseCase>()
            .FromFunction(c => new SetCameraAreaUseCase(
                c.Resolve<ICameras2dService>(),
                c.Resolve<CameraBehavioursData>()
            ));

        builder.Bind<ShakeCameraUseCase>()
            .FromFunction(c => new ShakeCameraUseCase(
                c.Resolve<CameraBehavioursData>(),
                c.Resolve<GameCamerasConfiguration>()
            ));
    }
}