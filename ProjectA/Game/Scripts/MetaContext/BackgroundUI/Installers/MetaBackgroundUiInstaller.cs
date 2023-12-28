using Game.MetaContext.BackgroundUI.Interactors;
using Godot;
using GUtils.Di.Builder;
using GUtils.Di.Installers;
using GUtilsGodot.UiStack.Extensions;
using GUtilsGodot.Visibility.Visibles;

namespace Game.MetaContext.BackgroundUI.Installers;

public partial class MetaBackgroundUiInstaller : Control, IInstaller
{
    [Export] public AnimationPlayer? AnimationPlayer;
    
    public void Install(IDiContainerBuilder builder)
    {
        builder.Bind<IBackgroundUiInteractor>()
            .FromFunction(c => new BackgroundUiInteractor())
            .LinkUiToUiStackService(
                new AnimationPlayerVisible(AnimationPlayer!, "Show", "Hide"),
                this
            );
    }
}