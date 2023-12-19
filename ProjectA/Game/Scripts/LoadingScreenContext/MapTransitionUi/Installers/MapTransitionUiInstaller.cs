using Game.LoadingScreenContext.MapTransitionUi.Interactors;
using Game.LoadingScreenContext.MapTransitionUi.UseCases;
using Godot;
using GUtils.Di.Builder;
using GUtilsGodot.Di.Installers;
using GUtilsGodot.UiStack.Extensions;

namespace Game.LoadingScreenContext.MapTransitionUi.Installers;

public partial class MapTransitionUiInstaller : NodeInstaller
{
    [Export] public Control? Pivot;
    [Export] public Control? Panel;
    
    public sealed override void Install(IDiContainerBuilder builder)
    {
        builder.Bind<IMapTransitionUiInteractor>()
            .FromFunction(c => new MapTransitionUiInteractor(
            c.Resolve<SetVisibleUseCase>()    
            ))
            .LinkLoadingScreenToUiStackService(
                b => b.Resolve<IMapTransitionUiInteractor>(),
                this
            );
        
        builder.Bind<SetVisibleUseCase>()
            .FromFunction(c => new SetVisibleUseCase(
                Pivot!,
                Panel!
            ));
    }
}