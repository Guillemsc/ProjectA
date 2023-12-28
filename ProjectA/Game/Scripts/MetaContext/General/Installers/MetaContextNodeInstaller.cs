using Game.MetaContext.BackgroundUI.Installers;
using Game.MetaContext.IntroUi.Installers;
using Game.MetaContext.MainMenuUi.Installers;
using Godot;
using GUtils.Di.Builder;
using GUtilsGodot.Di.Installers;

namespace Game.MetaContext.General.Installers;

public partial class MetaContextNodeInstaller : NodeInstaller
{
    [Export] public MetaBackgroundUiInstaller? BackgroundUi;
    [Export] public MetaIntroUiInstaller? IntroUi;
    [Export] public MetaMainMenuUiInstaller? MainMenuUi;
    
    public override void Install(IDiContainerBuilder builder)
    {
        builder.Install(BackgroundUi!);
        builder.Install(IntroUi!);
        builder.Install(MainMenuUi!);
    }
}