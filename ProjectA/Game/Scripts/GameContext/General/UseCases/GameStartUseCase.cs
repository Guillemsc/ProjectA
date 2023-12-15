using Game.GameContext.Players.UseCases;

namespace Game.GameContext.General.UseCases;

public sealed class GameStartUseCase
{
    readonly SetPlayerMovementEnabledUseCase _setPlayerMovementEnabledUseCase;

    public GameStartUseCase(
        SetPlayerMovementEnabledUseCase setPlayerMovementEnabledUseCase
        )
    {
        _setPlayerMovementEnabledUseCase = setPlayerMovementEnabledUseCase;
    }

    public void Execute()
    {
        _setPlayerMovementEnabledUseCase.Execute(true);
    }
}