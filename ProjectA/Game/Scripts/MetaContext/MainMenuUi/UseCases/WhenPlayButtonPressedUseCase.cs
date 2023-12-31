using Game.GameContext.General.ApplicationContexts;
using Game.GameContext.General.Configurations;
using Game.MetaContext.General.ApplicationContexts;
using Game.MetaContext.MainMenuUi.Interactors;
using GUtils.Directions;
using GUtils.Executables;
using GUtils.Loading.Extensions;
using GUtils.Loading.Services;
using GUtilsGodot.UiStack.Services;

namespace Game.MetaContext.MainMenuUi.UseCases;

public sealed class WhenPlayButtonPressedUseCase : IExecutable
{
    readonly ILoadingService _loadingService;
    readonly IUiStackService _uiStackService;
    readonly GameConfiguration _gameConfiguration;
    readonly IMainMenuUiInteractor _mainMenuUiInteractor;

    public WhenPlayButtonPressedUseCase(
        ILoadingService loadingService, 
        IUiStackService uiStackService,
        GameConfiguration gameConfiguration, 
        IMainMenuUiInteractor mainMenuUiInteractor
        )
    {
        _loadingService = loadingService;
        _uiStackService = uiStackService;
        _gameConfiguration = gameConfiguration;
        _mainMenuUiInteractor = mainMenuUiInteractor;
    }

    public void Execute()
    {
        _uiStackService.SetNotInteractableNow(_mainMenuUiInteractor);
        
        GameApplicationContextConfiguration contextConfiguration = new(
            _gameConfiguration.MapsConfiguration!.TestMap!,
            string.Empty,
            true,
            HorizontalDirection.Right
        );
        
        _loadingService.New()
            .EnqueueUnloadApplicationContext<MetaApplicationContext>()
            .EnqueueLoadAndStartApplicationContext(new GameApplicationContext(contextConfiguration))
            .ExecuteAsync();
    }
}