using System.Threading;
using Game.GameContext.OutroUi.Interactors;
using Game.GameContext.OutroUi.UseCases;
using Godot;
using GUtils.Di.Builder;
using GUtilsGodot.Di.Installers;
using GUtils.Extensions;

namespace Game.GameContext.OutroUi.Installers;

public partial class GameOutroUiInstaller : ControlInstaller
{
    [Export] public AnimationPlayer? AnimationPlayer;
    
    public override void Install(IDiContainerBuilder builder)
    {
        builder.Bind<IOutroUiInteractor>()
            .FromFunction(c => new OutroUiInteractor(
                c.Resolve<SetVisibleUseCase>()
            ));
        
        builder.Bind<SetVisibleUseCase>()
            .FromFunction(c => new SetVisibleUseCase(
                AnimationPlayer!
            ))
            .WhenInit(o => o.Execute(false, true, CancellationToken.None).RunAsync)
            .NonLazy();
    }
}