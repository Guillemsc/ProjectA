using Game.GameContext.Cinematics.UseCases;
using Game.GameContext.Dialogues.UseCases;

namespace Game.GameContext.Cinematics.Datas;

public sealed class CinematicsMethods
{
    public AwaitUntilPlayerIsOnTheGroundUseCase AwaitUntilPlayerIsOnTheGroundUseCase { get; }
    public PlayDialogueUseCase PlayDialogueUseCase { get; }
    
    public CinematicsMethods(
        AwaitUntilPlayerIsOnTheGroundUseCase awaitUntilPlayerIsOnTheGroundUseCase, 
        PlayDialogueUseCase playDialogueUseCase
        )
    {
        AwaitUntilPlayerIsOnTheGroundUseCase = awaitUntilPlayerIsOnTheGroundUseCase;
        PlayDialogueUseCase = playDialogueUseCase;

    }
}