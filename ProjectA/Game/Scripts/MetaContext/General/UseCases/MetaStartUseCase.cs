using Game.MetaContext.BackgroundUI.Interactors;
using Game.MetaContext.General.Configurations;
using Game.MetaContext.IntroUi.Interactors;
using Game.MetaContext.MainMenuUi.Interactors;
using GUtilsGodot.UiStack.Services;

namespace Game.MetaContext.General.UseCases;

public sealed class MetaStartUseCase
{
    readonly MetaApplicationContextConfiguration _contextConfiguration;
    readonly IUiStackService _uiStackService;
    readonly IBackgroundUiInteractor _backgroundUiInteractor;
    readonly IIntroUiInteractor _introUiInteractor;
    readonly IMainMenuUiInteractor _mainMenuUiInteractor;

    public MetaStartUseCase(
        MetaApplicationContextConfiguration contextConfiguration,
        IUiStackService uiStackService, 
        IBackgroundUiInteractor backgroundUiInteractor,
        IIntroUiInteractor introUiInteractor, 
        IMainMenuUiInteractor mainMenuUiInteractor
        )
    {
        _contextConfiguration = contextConfiguration;
        _uiStackService = uiStackService;
        _backgroundUiInteractor = backgroundUiInteractor;
        _introUiInteractor = introUiInteractor;
        _mainMenuUiInteractor = mainMenuUiInteractor;
    }

    public void Execute()
    {
        if (!_contextConfiguration.PlayIntro)
        {
            return;
        }
        
        _uiStackService.New()
            .Show(_backgroundUiInteractor)
            .Show(_introUiInteractor)
            .HideCurrent()
            .Show(_mainMenuUiInteractor)
            .ExecuteAsync();
    }
}