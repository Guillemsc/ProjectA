using Godot;

namespace Game.GameContext.Pause.Configurations;

[GlobalClass]
public partial class GamePauseConfiguration : Resource
{
    [Export] public float PauseGameLogicSomeFramesDurationSeconds = 0.05f;
}