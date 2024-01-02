using Game.GameContext.Dialogues.Configurations;
using Game.GameContext.Dialogues.Datas;
using Game.GameContext.Dialogues.UseCases;
using Game.GameContext.DialogueUi.Interactors;
using GUtils.Di.Builder;

namespace Game.GameContext.Dialogues.Installers;

public static class GameDialoguesInstaller
{
    public static void InstallGameDialogues(this IDiContainerBuilder builder)
    {
        builder.Bind<CurrentDialogueData>().FromNew();
        
        builder.Bind<PlayDialogueUseCase>()
            .FromFunction(c => new PlayDialogueUseCase(
                c.Resolve<IDialogueUiInteractor>(),
                c.Resolve<CurrentDialogueData>(),
                c.Resolve<PlayDialogueEntryUseCase>(),
                c.Resolve<AwaitDialogueContinueInputUseCase>(),
                c.Resolve<GetDialogueSpeakerConfigurationUseCase>()
            ));

        builder.Bind<PlayDialogueEntryUseCase>()
            .FromFunction(c => new PlayDialogueEntryUseCase(
                c.Resolve<IDialogueUiInteractor>()
            ));

        builder.Bind<AwaitDialogueContinueInputUseCase>()
            .FromFunction(c => new AwaitDialogueContinueInputUseCase());

        builder.Bind<GetDialogueSpeakerConfigurationUseCase>()
            .FromFunction(c => new GetDialogueSpeakerConfigurationUseCase(
                c.Resolve<GameDialoguesConfiguration>()
            ));
    }
}