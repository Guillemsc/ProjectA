using Game.GameContext.AngryBlocks.Views;
using GUtils.Pooling.Objects;

namespace Game.GameContext.AngryBlocks.UseCases;

public sealed class TickAngryBlockViewUseCase : IReturnablePooledObject
{
    TickAngryBlockBlinkUseCase? _tickAngryBlockBlinkUseCase;
    TickAngryBlockMovementUseCase? _tickAngryBlockMovementUseCase;

    public void Init(
        TickAngryBlockBlinkUseCase tickAngryBlockBlinkUseCase, 
        TickAngryBlockMovementUseCase tickAngryBlockMovementUseCase
        )
    {
        _tickAngryBlockBlinkUseCase = tickAngryBlockBlinkUseCase;
        _tickAngryBlockMovementUseCase = tickAngryBlockMovementUseCase;
    }

    public void PooledObjectReturned()
    {
        _tickAngryBlockBlinkUseCase = null;
        _tickAngryBlockMovementUseCase = null;
    }
    
    public void Execute(AngryBlockView angryBlockView)
    {
        _tickAngryBlockBlinkUseCase!.Execute(angryBlockView);
        _tickAngryBlockMovementUseCase!.Execute(angryBlockView);
    }
}