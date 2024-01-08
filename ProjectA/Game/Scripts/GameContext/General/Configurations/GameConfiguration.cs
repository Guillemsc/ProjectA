using Game.GameContext.Cameras.Configurations;
using Game.GameContext.Collectables.Configurations;
using Game.GameContext.Crates.Configurations;
using Game.GameContext.Dialogues.Configurations;
using Game.GameContext.Maps.Configurations;
using Game.GameContext.Pause.Configurations;
using Game.GameContext.Players.Configurations;
using Game.GameContext.VisualEffects.Configurations;
using Godot;

namespace Game.GameContext.General.Configurations;

[GlobalClass]
public partial class GameConfiguration : Resource
{
    [Export] public GamePlayersConfiguration? PlayersConfiguration;
    [Export] public GameMapsConfiguration? MapsConfiguration;
    [Export] public GameCamerasConfiguration? CamerasConfiguration;
    [Export] public GameVisualEffectsConfiguration? VisualEffectsConfiguration;
    [Export] public GameCollectablesConfiguration? CollectablesConfiguration;
    [Export] public GameCratesConfiguration? CratesConfiguration;
    [Export] public GameDialoguesConfiguration? DialoguesConfiguration;
    [Export] public GamePauseConfiguration? GamePauseConfiguration;
}