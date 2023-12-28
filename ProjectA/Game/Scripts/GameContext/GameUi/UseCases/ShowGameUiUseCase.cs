using Game.GameContext.GameUi.Interactors;
using GUtilsGodot.UiStack.Services;

namespace Game.GameContext.GameUi.UseCases;

public sealed class ShowGameUiUseCase
{
    readonly IUiStackService _uiStackService;
    readonly IGameUiInteractor _gameUiInteractor;

    public ShowGameUiUseCase(
        IUiStackService uiStackService, 
        IGameUiInteractor gameUiInteractor
        )
    {
        _uiStackService = uiStackService;
        _gameUiInteractor = gameUiInteractor;
    }

    public void Execute()
    {
        _uiStackService.New()
            .Show(_gameUiInteractor, true)
            .ExecuteAsync();
    }
}