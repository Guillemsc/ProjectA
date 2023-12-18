using GUtilsGodot.Cameras.Services;

namespace Game.GameContext.Cameras.UseCases;

public sealed class DisposeCameraSetupUseCase
{
    readonly ICameras2dService _cameras2dService;

    public DisposeCameraSetupUseCase(ICameras2dService cameras2dService)
    {
        _cameras2dService = cameras2dService;
    }

    public void Execute()
    {
        _cameras2dService.ClearBehaviours();
    }
}