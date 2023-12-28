using Game.MetaContext.IntroUi.Datas;
using Game.MetaContext.IntroUi.Interactors;
using Game.MetaContext.IntroUi.UseCases;
using Godot;
using GUtils.Di.Builder;
using GUtils.Di.Installers;
using GUtils.Tick.Extensions;
using GUtilsGodot.UiStack.Extensions;
using GUtilsGodot.Visibility.Visibles;

namespace Game.MetaContext.IntroUi.Installers;

public partial class MetaIntroUiInstaller : Control, IInstaller
{
    [Export] public AnimationPlayer? AnimationPlayer;
    
    public void Install(IDiContainerBuilder builder)
    {
        builder.Bind<IIntroUiInteractor>()
            .FromFunction(c => new IntroUiInteractor(
                c.Resolve<SetVisibleUseCase>()
            ))
            .LinkUiToUiStackService(
                c => c.Resolve<IIntroUiInteractor>(),
                this
            );

        builder.Bind<IntroUiStopPlayingData>().FromNew();

        builder.Bind<ShowUseCase>()
            .FromFunction(c => new ShowUseCase(
                AnimationPlayer!,
                c.Resolve<IntroUiStopPlayingData>()
            ));

        builder.Bind<HideUseCase>()
            .FromFunction(c => new HideUseCase(
                AnimationPlayer!
            ));

        builder.Bind<SetVisibleUseCase>()
            .FromFunction(c => new SetVisibleUseCase(
                c.Resolve<ShowUseCase>(),
                c.Resolve<HideUseCase>()
            ));

        builder.Bind<TickIntroInputForCancellationUseCase>()
            .FromFunction(c => new TickIntroInputForCancellationUseCase(
                c.Resolve<IntroUiStopPlayingData>()
            ))
            .LinkToTickablesService(o => o.Execute);
    }
}