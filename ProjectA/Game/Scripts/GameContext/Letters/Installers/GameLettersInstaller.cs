using Game.GameContext.Letters.UseCases;
using Game.GameContext.LetterUi.Interactors;
using Game.GameContext.Pause.UseCases;
using Game.GameContext.Players.Datas;
using Game.ServicesContext.Time.Services;
using GUtils.Di.Builder;
using GUtils.Tasks.Runners;

namespace Game.GameContext.Letters.Installers;

public static class GameLettersInstaller
{
    public static void InstallGameLetters(this IDiContainerBuilder builder)
    {
        builder.Bind<WhenLetterCollectableCollectedUseCase>()
            .FromFunction(c => new WhenLetterCollectableCollectedUseCase(
                c.Resolve<ShowLetterUseCase>()
            ));
        
        builder.Bind<ShowLetterUseCase>()
            .FromFunction(c => new ShowLetterUseCase(
                c.Resolve<IGameTimesService>(),
                c.Resolve<IAsyncTaskRunner>(),
                c.Resolve<ILetterUiInteractor>(),
                c.Resolve<PlayerViewData>(),
                c.Resolve<AwaitLetterContinueInputUseCase>()
            ));
        
        builder.Bind<AwaitLetterContinueInputUseCase>()
            .FromFunction(c => new AwaitLetterContinueInputUseCase(
            ));
    }
}