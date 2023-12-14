using Game.GameContext.General.Datas;
using Godot;
using GUtils.Di.Builder;
using GUtilsGodot.Di.Installers;

namespace Game.GameContext.General.Installers;

public partial class GameContextNodeInstaller : NodeInstaller
{
    public override void Install(IDiContainerBuilder builder)
    {
        builder.Bind<GameGeneralViewData>()
            .FromInstance(new GameGeneralViewData(this!));
    }
}