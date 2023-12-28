using Game.GameContext.GameUi.Installers;
using Game.GameContext.General.Datas;
using Godot;
using GUtils.Di.Builder;
using GUtilsGodot.Di.Installers;

namespace Game.GameContext.General.Installers;

public partial class GameContextNodeInstaller : NodeInstaller
{
    [Export] public GameUiInstaller? GameUi;
    
    public override void Install(IDiContainerBuilder builder)
    {
        builder.Install(GameUi!);
        
        builder.Bind<GameGeneralViewData>()
            .FromInstance(new GameGeneralViewData(this!));
    }
}