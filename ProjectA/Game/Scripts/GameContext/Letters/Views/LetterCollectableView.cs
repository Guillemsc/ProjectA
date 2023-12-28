using Game.GameContext.Collectables.Views;
using Game.GameContext.Letters.Configurations;
using Godot;

namespace Game.GameContext.Letters.Views;

public partial class LetterCollectableView : CollectableView
{
    [Export] public LetterConfiguration? LetterConfiguration;
}