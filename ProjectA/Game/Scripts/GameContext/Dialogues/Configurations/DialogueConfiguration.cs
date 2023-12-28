using Godot;

namespace Game.GameContext.Dialogues.Configurations;

[GlobalClass]
public partial class DialogueConfiguration : Resource
{
    [Export] public DialogueEntryConfiguration[]? Entries;
}