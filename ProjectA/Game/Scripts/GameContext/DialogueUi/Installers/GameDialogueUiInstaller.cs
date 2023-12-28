using System.Threading;
using Game.GameContext.DialogueUi.Data;
using Game.GameContext.DialogueUi.Interactors;
using Game.GameContext.DialogueUi.UseCases;
using Godot;
using GUtils.Di.Builder;
using GUtilsGodot.Di.Installers;

namespace Game.GameContext.DialogueUi.Installers;

public partial class GameDialogueUiInstaller : ControlInstaller
{
    [Export] public AnimationPlayer? AnimationPlayer;
    [Export] public RichTextLabel? DialogueLabel;
    [Export] public Control? DialogueShownIndicatorControl;
    [Export] public AnimationPlayer? DialogueShownIndicatorAnimationPlayer;
    [Export] public float DialogueDurationPerWord = 0.05f;
    [Export] public Control? DialoguePositionControl;
    [Export] public Control? LeftDialoguePosition;
    [Export] public Control? RightDialoguePosition;
    [Export] public TextureRect? LeftPortraitImage;
    [Export] public TextureRect? RightPortraitImage;
    
    public override void Install(IDiContainerBuilder builder)
    {
        builder.Bind<IDialogueUiInteractor>()
            .FromFunction(c => new DialogueUiInteractor(
                c.Resolve<SetVisibleUseCase>(),
                c.Resolve<ShowTextUseCase>(),
                c.Resolve<SetupDialogueUseCase>()
            ));

        builder.Bind<DialogueUiVisibilityData>().FromNew();
        builder.Bind<DialogueUiTweensData>().FromNew();
        
        builder.Bind<SetVisibleUseCase>()
            .FromFunction(c => new SetVisibleUseCase(
                c.Resolve<DialogueUiVisibilityData>(),
                AnimationPlayer!,
                c.Resolve<ClearTextUseCase>()
            ))
            .WhenInit(o => o.Execute(false, true, CancellationToken.None))
            .NonLazy();

        builder.Bind<ClearTextUseCase>()
            .FromFunction(c => new ClearTextUseCase(
                DialogueLabel!,
                DialogueShownIndicatorControl!
            ));

        builder.Bind<ShowTextUseCase>()
            .FromFunction(c => new ShowTextUseCase(
                c.Resolve<DialogueUiTweensData>(),
                DialogueLabel!,
                DialogueShownIndicatorControl!,
                DialogueShownIndicatorAnimationPlayer!,
                DialogueDurationPerWord
            ));

        builder.Bind<SetupDialogueUseCase>()
            .FromFunction(c => new SetupDialogueUseCase(
                DialoguePositionControl!,
                LeftDialoguePosition!,
                RightDialoguePosition!,
                LeftPortraitImage!,
                RightPortraitImage!
            ));
    }
}