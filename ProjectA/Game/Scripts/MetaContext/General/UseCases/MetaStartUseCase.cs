using Game.MetaContext.BackgroundUI.Interactors;
using Game.MetaContext.IntroUi.Interactors;
using Game.MetaContext.MainMenuUi.Interactors;
using GUtilsGodot.UiStack.Services;

namespace Game.MetaContext.General.UseCases;

public sealed class MetaStartUseCase
{
    readonly IUiStackService _uiStackService;
    readonly IBackgroundUiInteractor _backgroundUiInteractor;
    readonly IIntroUiInteractor _introUiInteractor;
    readonly IMainMenuUiInteractor _mainMenuUiInteractor;

    public MetaStartUseCase(
        IUiStackService uiStackService, 
        IBackgroundUiInteractor backgroundUiInteractor,
        IIntroUiInteractor introUiInteractor, 
        IMainMenuUiInteractor mainMenuUiInteractor
        )
    {
        _uiStackService = uiStackService;
        _backgroundUiInteractor = backgroundUiInteractor;
        _introUiInteractor = introUiInteractor;
        _mainMenuUiInteractor = mainMenuUiInteractor;
    }

    public void Execute()
    {
        _uiStackService.New()
            .Show(_backgroundUiInteractor)
            .Show(_introUiInteractor)
            .HideCurrent()
            .Show(_mainMenuUiInteractor)
            .ExecuteAsync();
    }
}