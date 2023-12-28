using Godot;

namespace Game.GameContext.Letters.Configurations;

[GlobalClass]
public partial class LetterConfiguration : Resource
{
    [Export(PropertyHint.MultilineText)] public string? Text = "Placeholder";
}