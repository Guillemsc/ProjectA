using Game.GameContext.Areas.UseCases;
using Game.GameContext.Areas.Views;
using GUtils.Optionals;

namespace Game.GameContext.Cameras.UseCases;

public sealed class SetInitialCameraAreaUseCase
{
    readonly GetCurrentPlayerAreaUseCase _getCurrentPlayerAreaUseCase;
    readonly SetCameraAreaUseCase _setCameraAreaUseCase;

    public SetInitialCameraAreaUseCase(
        GetCurrentPlayerAreaUseCase getCurrentPlayerAreaUseCase,
        SetCameraAreaUseCase setCameraAreaUseCase
        )
    {
        _getCurrentPlayerAreaUseCase = getCurrentPlayerAreaUseCase;
        _setCameraAreaUseCase = setCameraAreaUseCase;
    }

    public void Execute()
    {
        Optional<AreaView> optionalAreaView = _getCurrentPlayerAreaUseCase.Execute();

        bool hasArea = optionalAreaView.TryGet(out AreaView areaView);

        if (!hasArea)
        {
            return;
        }
        
        _setCameraAreaUseCase.Execute(areaView);
    }
}