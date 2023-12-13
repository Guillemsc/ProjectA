using Game.LoadingScreenContext.LoadingScreenUi.Interactors;
using Game.LoadingScreenContext.LoadingScreenUi.UseCases;
using Godot;
using GUtils.Di.Builder;
using GUtils.Loading.Services;
using GUtilsGodot.Di.Installers;
using GUtilsGodot.UiStack.Extensions;
using GUtilsGodot.UiStack.Services;
using GUtilsGodot.Visibility.Visibles;

namespace Game.LoadingScreenContext.LoadingScreenUi.Installers;

public partial class LoadingScreenUiInstaller : NodeInstaller
{
    [Export] public AnimationPlayer? AnimationPlayer;
    
    public sealed override void Install(IDiContainerBuilder builder)
    {
        builder.Bind<ILoadingScreenUiInteractor>()
            .FromFunction(c => new LoadingScreenUiInteractor())
            .LinkLoadingScreenToUiStackService(
                new AnimationPlayerVisible(AnimationPlayer!, "ShowAnimation", "HideAnimation"),
                this
            );

        builder.Bind<LinkToLoadingServiceUseCase>()
            .FromFunction(c => new LinkToLoadingServiceUseCase(
                c.Resolve<ILoadingService>(),
                c.Resolve<IUiStackService>(),
                c.Resolve<ILoadingScreenUiInteractor>()
            ))
            .WhenInit(o => o.Execute)
            .NonLazy();
    }
}