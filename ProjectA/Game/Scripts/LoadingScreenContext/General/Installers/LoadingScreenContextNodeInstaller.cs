using Game.LoadingScreenContext.LoadingScreenUi.Installers;
using Game.LoadingScreenContext.MapTransitionUi.Installers;
using Godot;
using GUtils.Di.Builder;
using GUtilsGodot.Di.Installers;

namespace Game.LoadingScreenContext.General.Installers;

public partial class LoadingScreenContextNodeInstaller : NodeInstaller
{
    [Export] public LoadingScreenUiInstaller? LoadingScreenUiInstaller;
    [Export] public MapTransitionUiInstaller? MapTransitionUiInstaller;
    
    public sealed override void Install(IDiContainerBuilder builder)
    {
        builder.Install(LoadingScreenUiInstaller!);
        builder.Install(MapTransitionUiInstaller!);
    }
}