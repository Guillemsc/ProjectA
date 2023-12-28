using Game.GameContext.DialogueUi.Installers;
using Game.GameContext.GameUi.Interactors;
using Game.GameContext.GameUi.UseCases;
using Game.GameContext.LetterUi.Installers;
using Godot;
using GUtils.Di.Builder;
using GUtilsGodot.Di.Installers;
using GUtilsGodot.UiStack.Extensions;
using GUtilsGodot.UiStack.Services;
using GUtilsGodot.Visibility.Visibles;

namespace Game.GameContext.GameUi.Installers;

public partial class GameUiInstaller : ControlInstaller
{
    [Export] public GameDialogueUiInstaller? DialogueUi;
    [Export] public GameLetterUiInstaller? LetterUi;
    
    public override void Install(IDiContainerBuilder builder)
    {
        builder.Bind<IGameUiInteractor>()
            .FromFunction(c => new GameUiInteractor())
            .LinkUiToUiStackService(
                new ModulateAlphaControlVisible(this),
                this
            );

        builder.Bind<ShowGameUiUseCase>()
            .FromFunction(c => new ShowGameUiUseCase(
                c.Resolve<IUiStackService>(),
                c.Resolve<IGameUiInteractor>()
            ));

        builder.Install(DialogueUi!);
        builder.Install(LetterUi!);
    }
}