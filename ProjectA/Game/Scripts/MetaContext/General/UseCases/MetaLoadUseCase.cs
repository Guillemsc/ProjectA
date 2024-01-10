using System.Threading;
using System.Threading.Tasks;
using Game.MetaContext.BackgroundUI.Interactors;
using Game.MetaContext.General.Configurations;
using Game.MetaContext.IntroUi.Interactors;
using Game.MetaContext.MainMenuUi.Interactors;
using GUtilsGodot.UiStack.Services;

namespace Game.MetaContext.General.UseCases;

public sealed class MetaLoadUseCase
{
    readonly MetaApplicationContextConfiguration _contextConfiguration;
    readonly IUiStackService _uiStackService;
    readonly IBackgroundUiInteractor _backgroundUiInteractor;
    readonly IMainMenuUiInteractor _mainMenuUiInteractor;

    public MetaLoadUseCase(
        MetaApplicationContextConfiguration contextConfiguration, 
        IUiStackService uiStackService, 
        IBackgroundUiInteractor backgroundUiInteractor, 
        IMainMenuUiInteractor mainMenuUiInteractor
        )
    {
        _contextConfiguration = contextConfiguration;
        _uiStackService = uiStackService;
        _backgroundUiInteractor = backgroundUiInteractor;
        _mainMenuUiInteractor = mainMenuUiInteractor;
    }

    public Task Execute(CancellationToken cancellationToken)
    {
        if (_contextConfiguration.PlayIntro)
        {
            return Task.CompletedTask;
        }
        
        return _uiStackService.New()
            .Show(_backgroundUiInteractor, true)
            .Show(_mainMenuUiInteractor, true)
            .Execute(cancellationToken);
    }
}