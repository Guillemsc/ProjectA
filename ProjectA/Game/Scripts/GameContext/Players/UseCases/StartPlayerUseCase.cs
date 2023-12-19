using Game.GameContext.General.Configurations;

namespace Game.GameContext.Players.UseCases;

public sealed class StartPlayerUseCase
{
    readonly GameApplicationContextConfiguration _gameApplicationContextConfiguration;
    readonly AppearPlayerUseCase _appearPlayerUseCase;
    readonly EnablePlayerUseCase _enablePlayerUseCase;

    public StartPlayerUseCase(
        GameApplicationContextConfiguration gameApplicationContextConfiguration,
        AppearPlayerUseCase appearPlayerUseCase, 
        EnablePlayerUseCase enablePlayerUseCase
        )
    {
        _gameApplicationContextConfiguration = gameApplicationContextConfiguration;
        _appearPlayerUseCase = appearPlayerUseCase;
        _enablePlayerUseCase = enablePlayerUseCase;
    }

    public void Execute()
    {
        if (_gameApplicationContextConfiguration.PlayerAppears)
        {
            _appearPlayerUseCase.Execute();
        }
        else
        {
            _enablePlayerUseCase.Execute();
        }
    }
}