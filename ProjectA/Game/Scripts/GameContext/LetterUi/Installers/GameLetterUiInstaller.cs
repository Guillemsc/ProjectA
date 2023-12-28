using System.Threading;
using Game.GameContext.LetterUi.Interactors;
using Game.GameContext.LetterUi.UseCases;
using Godot;
using GUtils.Di.Builder;
using GUtils.Extensions;
using GUtilsGodot.Di.Installers;

namespace Game.GameContext.LetterUi.Installers;

public partial class GameLetterUiInstaller : ControlInstaller
{
    [Export] public AnimationPlayer? AnimationPlayer;
    [Export] public Label? TextLabel;
    
    public override void Install(IDiContainerBuilder builder)
    {
        builder.Bind<ILetterUiInteractor>()
            .FromFunction(c => new LetterUiInteractor(
                c.Resolve<SetVisibleUseCase>(),
                c.Resolve<SetTextUseCase>()
            ));
        
        builder.Bind<SetVisibleUseCase>()
            .FromFunction(c => new SetVisibleUseCase(
                AnimationPlayer!
            ))
            .WhenInit(o => o.Execute(false, true, CancellationToken.None).RunAsync)
            .NonLazy();

        builder.Bind<SetTextUseCase>()
            .FromFunction(c => new SetTextUseCase(
                TextLabel!
            ));
    }
}