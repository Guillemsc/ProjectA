using Game.MetaContext.MainMenuUi.Data;
using Godot;
using GUtils.Executables;
using GUtilsGodot.Extensions;

namespace Game.MetaContext.MainMenuUi.UseCases;

public sealed class FoldInstantlyUseCase : IExecutable
{
    readonly AnimationPlayer _foldingAnimationPlayer;
    readonly FoldedData _foldedData;

    public FoldInstantlyUseCase(
        AnimationPlayer foldingAnimationPlayer, 
        FoldedData foldedData
        )
    {
        _foldingAnimationPlayer = foldingAnimationPlayer;
        _foldedData = foldedData;
    }

    public void Execute()
    {
        _foldedData.Folded = true;
        
        _foldingAnimationPlayer.Play("Fold", true);
    }
}