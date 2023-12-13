using System.Threading;
using System.Threading.Tasks;
using Game.LoadingScreenContext.LoadingScreenUi.Interactors;
using GUtils.Loading.Services;
using GUtilsGodot.UiStack.Services;

namespace Game.LoadingScreenContext.LoadingScreenUi.UseCases;

public sealed class LinkToLoadingServiceUseCase
{
    readonly ILoadingService _loadingService;
    readonly IUiStackService _uiStackService;
    readonly ILoadingScreenUiInteractor _loadingScreenUiInteractor;

    public LinkToLoadingServiceUseCase(
        ILoadingService loadingService, 
        IUiStackService uiStackService, 
        ILoadingScreenUiInteractor loadingScreenUiInteractor
        )
    {
        _loadingService = loadingService;
        _uiStackService = uiStackService;
        _loadingScreenUiInteractor = loadingScreenUiInteractor;
    }

    public void Execute()
    {
        Task Show(bool instantly, CancellationToken cancellationToken)
        {
            return _uiStackService.New()
                .Show(_loadingScreenUiInteractor, instantly)
                .Execute(cancellationToken);
        }
        
        Task Hide(bool instantly, CancellationToken cancellationToken)
        {
            return _uiStackService.New()
                .Hide(_loadingScreenUiInteractor, instantly)
                .Execute(cancellationToken);
        }
        
        _loadingService.AddBeforeLoading(Show);
        _loadingService.AddAfterLoading(Hide);
    }
}