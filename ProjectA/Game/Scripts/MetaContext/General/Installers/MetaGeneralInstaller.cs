using Game.MetaContext.BackgroundUI.Interactors;
using Game.MetaContext.General.Interactors;
using Game.MetaContext.General.UseCases;
using Game.MetaContext.IntroUi.Interactors;
using Game.MetaContext.MainMenuUi.Interactors;
using GUtils.Di.Builder;
using GUtils.Tasks.Extensions;
using GUtilsGodot.UiStack.Services;

namespace Game.MetaContext.General.Installers;

public static class MetaGeneralInstaller
{
    public static void InstallMetaGeneral(this IDiContainerBuilder builder)
    {
        builder.Bind<IMetaContextInteractor>()
            .FromFunction(c => new MetaContextInteractor(
                c.Resolve<MetaStartUseCase>()
            ));

        builder.Bind<MetaStartUseCase>()
            .FromFunction(c => new MetaStartUseCase(
                c.Resolve<IUiStackService>(),
                c.Resolve<IBackgroundUiInteractor>(),
                c.Resolve<IIntroUiInteractor>(),
                c.Resolve<IMainMenuUiInteractor>()
            ));
        
        builder.InstallAsyncTaskRunner();
    }
}