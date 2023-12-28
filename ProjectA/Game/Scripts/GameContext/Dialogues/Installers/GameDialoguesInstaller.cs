using Game.GameContext.Dialogues.UseCases;
using Game.GameContext.DialogueUi.Interactors;
using GUtils.Di.Builder;

namespace Game.GameContext.Dialogues.Installers;

public static class GameDialoguesInstaller
{
    public static void InstallGameDialogues(this IDiContainerBuilder builder)
    {
        builder.Bind<PlayDialogueUseCase>()
            .FromFunction(c => new PlayDialogueUseCase(
                c.Resolve<IDialogueUiInteractor>(),
                c.Resolve<PlayDialogueEntryUseCase>(),
                c.Resolve<AwaitDialogueContinueInputUseCase>()
            ));

        builder.Bind<PlayDialogueEntryUseCase>()
            .FromFunction(c => new PlayDialogueEntryUseCase(
                c.Resolve<IDialogueUiInteractor>()
            ));

        builder.Bind<AwaitDialogueContinueInputUseCase>()
            .FromFunction(c => new AwaitDialogueContinueInputUseCase());
    }
}