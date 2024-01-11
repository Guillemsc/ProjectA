using Game.GameContext.AngryBlocks.Views;

namespace Game.GameContext.AngryBlocks.UseCases;

public sealed class TickAngryBlockViewUseCase
{
    readonly TickAngryBlockBlinkUseCase _tickAngryBlockBlinkUseCase;
    readonly TickAngryBlockMovementUseCase _tickAngryBlockMovementUseCase;

    public TickAngryBlockViewUseCase(
        TickAngryBlockBlinkUseCase tickAngryBlockBlinkUseCase, 
        TickAngryBlockMovementUseCase tickAngryBlockMovementUseCase
        )
    {
        _tickAngryBlockBlinkUseCase = tickAngryBlockBlinkUseCase;
        _tickAngryBlockMovementUseCase = tickAngryBlockMovementUseCase;
    }

    public void Execute(AngryBlockView angryBlockView)
    {
        _tickAngryBlockBlinkUseCase.Execute(angryBlockView);
        _tickAngryBlockMovementUseCase.Execute(angryBlockView);
    }
}