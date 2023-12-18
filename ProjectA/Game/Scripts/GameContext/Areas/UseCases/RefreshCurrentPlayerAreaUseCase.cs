using Game.GameContext.Areas.Datas;
using Game.GameContext.Areas.Views;
using GUtils.Optionals;

namespace Game.GameContext.Areas.UseCases;

public sealed class RefreshCurrentPlayerAreaUseCase
{
    readonly AreasData _areasData;
    readonly GetCurrentPlayerAreaUseCase _getCurrentPlayerAreaUseCase;
    readonly WhenCurrentPlayerAreaChangesUseCase _whenCurrentPlayerAreaChangesUseCase;

    public RefreshCurrentPlayerAreaUseCase(
        AreasData areasData, 
        GetCurrentPlayerAreaUseCase getCurrentPlayerAreaUseCase,
        WhenCurrentPlayerAreaChangesUseCase whenCurrentPlayerAreaChangesUseCase
        )
    {
        _areasData = areasData;
        _getCurrentPlayerAreaUseCase = getCurrentPlayerAreaUseCase;
        _whenCurrentPlayerAreaChangesUseCase = whenCurrentPlayerAreaChangesUseCase;
    }

    public void Execute()
    {
        Optional<AreaView> optionalArea = _getCurrentPlayerAreaUseCase.Execute();

        bool hasArea = optionalArea.TryGet(out AreaView area);
        
        if (!hasArea)
        {
            return;
        }

        bool changed = true;

        bool hasCurrentArea = _areasData.CurrentArea.TryGet(out AreaView currentArea);

        if (hasCurrentArea)
        {
            if (currentArea == area)
            {
                changed = false;
            }
        }

        if (!changed)
        {
            return;
        }

        _areasData.CurrentArea = area;
        _whenCurrentPlayerAreaChangesUseCase.Execute(area);
    }
}