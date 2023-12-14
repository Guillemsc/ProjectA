using Game.GameContext.Maps.Configurations;
using Game.GameContext.Players.Configurations;
using Game.GameContext.VisualEffects.Configurations;
using Godot;

namespace Game.GameContext.General.Configurations;

[GlobalClass]
public partial class GameConfiguration : Resource
{
    [Export] public GamePlayersConfiguration? PlayersConfiguration;
    [Export] public GameMapsConfiguration? MapsConfiguration;
    [Export] public GameVisualEffectsConfiguration? VisualEffectsConfiguration;
}