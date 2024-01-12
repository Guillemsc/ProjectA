using GUtils.Saws.Views;

namespace Game.GameContext.Saws.UseCases;

public sealed class TickSawViewUseCase
{
    readonly TickSawMovementUseCase _tickSawMovementUseCase;

    public TickSawViewUseCase(
        TickSawMovementUseCase tickSawMovementUseCase
        )
    {
        _tickSawMovementUseCase = tickSawMovementUseCase;
    }

    public void Execute(SawView sawView)
    {
        _tickSawMovementUseCase.Execute(sawView);
    }
}