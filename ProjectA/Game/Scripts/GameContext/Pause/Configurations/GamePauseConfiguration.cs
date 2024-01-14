using Godot;

namespace Game.GameContext.Pause.Configurations;

[GlobalClass]
public partial class GamePauseConfiguration : Resource
{
    [Export] public float PauseGameLogicSomeFramesShortDurationSeconds = 0.04f;
    [Export] public float PauseGameLogicSomeFramesMediumDurationSeconds = 0.06f;
    [Export] public float PauseGameLogicSomeFramesLongDurationSeconds = 0.08f;
    [Export] public float PauseGameLogicSomeFramesVeryLongDurationSeconds = 0.3f;
}