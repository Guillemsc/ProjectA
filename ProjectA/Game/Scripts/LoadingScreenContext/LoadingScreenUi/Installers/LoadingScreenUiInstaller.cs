using Game.LoadingScreenContext.LoadingScreenUi.Interactors;
using Godot;
using GUtils.Di.Builder;
using GUtilsGodot.Di.Installers;
using GUtilsGodot.UiStack.Extensions;
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
    }
}