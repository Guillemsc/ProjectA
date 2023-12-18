using Game.GameContext.Areas.Views;
using Game.GameContext.Cameras.UseCases;

namespace Game.GameContext.Areas.UseCases;

public sealed class WhenCurrentPlayerAreaChangesUseCase
{
    readonly SetCameraAreaUseCase _setCameraAreaUseCase;

    public WhenCurrentPlayerAreaChangesUseCase(
        SetCameraAreaUseCase setCameraAreaUseCase
        )
    {
        _setCameraAreaUseCase = setCameraAreaUseCase;
    }

    public void Execute(AreaView areaView)
    {
        _setCameraAreaUseCase.Execute(areaView);
    }
}