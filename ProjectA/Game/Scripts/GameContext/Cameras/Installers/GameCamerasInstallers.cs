using Game.GameContext.Areas.UseCases;
using Game.GameContext.Cameras.Configurations;
using Game.GameContext.Cameras.Datas;
using Game.GameContext.Cameras.UseCases;
using Game.GameContext.Players.Datas;
using GUtils.Di.Builder;
using GUtilsGodot.Cameras.Services;

namespace Game.GameContext.Cameras.Installers;

public static class GameCamerasInstallers
{
    public static void InstallGameCameras(this IDiContainerBuilder builder)
    {
        builder.Bind<CameraBehavioursData>().FromNew();

        builder.Bind<SetupCameraUseCase>()
            .FromFunction(c => new SetupCameraUseCase(
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
    }
}