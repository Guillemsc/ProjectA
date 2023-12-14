using Game.GameContext.Player.Configurations;
using Godot;

namespace Game.GameContext.General.Configurations;

[GlobalClass]
public partial class GameConfiguration : Resource
{
    [Export] public GamePlayerConfiguration? PlayerConfiguration;
}