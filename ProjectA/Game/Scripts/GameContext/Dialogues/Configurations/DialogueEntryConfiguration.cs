using Godot;
using GUtils.Locations.Enums;

namespace Game.GameContext.Dialogues.Configurations;

[GlobalClass]
public partial class DialogueEntryConfiguration : Resource
{
    [Export(PropertyHint.MultilineText)] public string? Text = "Placeholder";
    [Export] public HorizontalLocation PortraitLocation = HorizontalLocation.Left;

}