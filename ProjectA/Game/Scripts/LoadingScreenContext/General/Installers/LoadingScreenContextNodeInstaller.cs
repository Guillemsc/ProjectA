using Game.LoadingScreenContext.LoadingScreenUi.Installers;
using Godot;
using GUtils.Di.Builder;
using GUtilsGodot.Di.Installers;

namespace Game.LoadingScreenContext.General.Installers;

public partial class LoadingScreenContextNodeInstaller : NodeInstaller
{
    [Export] public LoadingScreenUiInstaller LoadingScreenUiInstaller;
    
    public sealed override void Install(IDiContainerBuilder builder)
    {
        builder.Install(LoadingScreenUiInstaller);
    }
}