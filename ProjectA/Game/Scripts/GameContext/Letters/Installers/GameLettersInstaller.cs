using Game.GameContext.Letters.UseCases;
using Game.GameContext.LetterUi.Interactors;
using GUtils.Di.Builder;
using GUtils.Tasks.Runners;

namespace Game.GameContext.Letters.Installers;

public static class GameLettersInstaller
{
    public static void InstallGameLetters(this IDiContainerBuilder builder)
    {
        builder.Bind<ShowLetterUseCase>()
            .FromFunction(c => new ShowLetterUseCase(
                c.Resolve<IAsyncTaskRunner>(),
                c.Resolve<ILetterUiInteractor>(),
                c.Resolve<AwaitLetterContinueInputUseCase>()
            ));
        
        builder.Bind<AwaitLetterContinueInputUseCase>()
            .FromFunction(c => new AwaitLetterContinueInputUseCase(
            ));
    }
}