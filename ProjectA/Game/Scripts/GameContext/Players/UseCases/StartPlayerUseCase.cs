namespace Game.GameContext.Players.UseCases;

public sealed class StartPlayerUseCase
{
    readonly AppearPlayerUseCase _appearPlayerUseCase;
    readonly EnablePlayerUseCase _enablePlayerUseCase;
    readonly CanPlayerPlayAppearAnimationUseCase _canPlayerPlayAppearAnimationUseCase;

    public StartPlayerUseCase(
        AppearPlayerUseCase appearPlayerUseCase, 
        EnablePlayerUseCase enablePlayerUseCase,
        CanPlayerPlayAppearAnimationUseCase canPlayerPlayAppearAnimationUseCase
        )
    {
        _appearPlayerUseCase = appearPlayerUseCase;
        _enablePlayerUseCase = enablePlayerUseCase;
        _canPlayerPlayAppearAnimationUseCase = canPlayerPlayAppearAnimationUseCase;
    }

    public void Execute()
    {
        bool shouldAppear = _canPlayerPlayAppearAnimationUseCase.Execute();
        
        if (shouldAppear)
        {
            _appearPlayerUseCase.Execute();
        }
        else
        {
            _enablePlayerUseCase.Execute();
        }
    }
}