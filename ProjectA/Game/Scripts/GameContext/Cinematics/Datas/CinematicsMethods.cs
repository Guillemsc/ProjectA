using Game.GameContext.Cameras.UseCases;
using Game.GameContext.Cinematics.UseCases;
using Game.GameContext.Dialogues.UseCases;
using Game.GameContext.Outro.UseCases;

namespace Game.GameContext.Cinematics.Datas;

public sealed class CinematicsMethods
{
    public AwaitUntilPlayerIsOnTheGroundUseCase AwaitUntilPlayerIsOnTheGroundUseCase { get; }
    public PlayDialogueUseCase PlayDialogueUseCase { get; }
    public SetCameraTargetUseCase SetCameraTargetUseCase { get; }
    public SetPlayerAsCameraTargetUseCase SetPlayerAsCameraTargetUseCase { get; }
    public PlayOutroUseCase PlayOutroUseCase { get; }
    
    public CinematicsMethods(
        AwaitUntilPlayerIsOnTheGroundUseCase awaitUntilPlayerIsOnTheGroundUseCase, 
        PlayDialogueUseCase playDialogueUseCase, 
        SetCameraTargetUseCase setCameraTargetUseCase, 
        SetPlayerAsCameraTargetUseCase setPlayerAsCameraTargetUseCase, 
        PlayOutroUseCase playOutroUseCase
        )
    {
        AwaitUntilPlayerIsOnTheGroundUseCase = awaitUntilPlayerIsOnTheGroundUseCase;
        PlayDialogueUseCase = playDialogueUseCase;
        SetCameraTargetUseCase = setCameraTargetUseCase;
        SetPlayerAsCameraTargetUseCase = setPlayerAsCameraTargetUseCase;
        PlayOutroUseCase = playOutroUseCase;
    }
}