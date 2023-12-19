using System.Threading;
using System.Threading.Tasks;
using Game.LoadingScreenContext.LoadingScreenUi.Interactors;
using Game.LoadingScreenContext.MapTransitionUi.Interactors;
using Game.ServicesContext.LoadingScreen.Enums;
using Game.ServicesContext.LoadingScreen.Services;
using GUtils.Loading.Services;
using GUtilsGodot.UiStack.Services;

namespace Game.LoadingScreenContext.General.UseCases;

public sealed class LinkToLoadingServiceUseCase
{
    readonly ILoadingService _loadingService;
    readonly IUiStackService _uiStackService;
    readonly ILoadingScreenService _loadingScreenService;
    readonly ILoadingScreenUiInteractor _loadingScreenUiInteractor;
    readonly IMapTransitionUiInteractor _mapTransitionUiInteractor;

    public LinkToLoadingServiceUseCase(
        ILoadingService loadingService, 
        IUiStackService uiStackService, 
        ILoadingScreenService loadingScreenService,
        ILoadingScreenUiInteractor loadingScreenUiInteractor, 
        IMapTransitionUiInteractor mapTransitionUiInteractor
        )
    {
        _loadingService = loadingService;
        _uiStackService = uiStackService;
        _loadingScreenService = loadingScreenService;
        _loadingScreenUiInteractor = loadingScreenUiInteractor;
        _mapTransitionUiInteractor = mapTransitionUiInteractor;
    }

    public void Execute()
    {
        Task Show(bool instantly, CancellationToken cancellationToken)
        {
            object toUse = _loadingScreenService.LoadingScreenToUse == LoadingScreenType.Default
                ? _loadingScreenUiInteractor
                : _mapTransitionUiInteractor;
            
            return _uiStackService.New()
                .Show(toUse, instantly)
                .Execute(cancellationToken);
        }
        
        Task Hide(bool instantly, CancellationToken cancellationToken)
        {
            object toUse = _loadingScreenService.LoadingScreenToUse == LoadingScreenType.Default
                ? _loadingScreenUiInteractor
                : _mapTransitionUiInteractor;
            
            return _uiStackService.New()
                .Hide(toUse, instantly)
                .Execute(cancellationToken);
        }
        
        _loadingService.AddBeforeLoading(Show);
        _loadingService.AddAfterLoading(Hide);
    }
}