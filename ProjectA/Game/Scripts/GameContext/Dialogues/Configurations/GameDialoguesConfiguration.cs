using Godot;

namespace Game.GameContext.Dialogues.Configurations;

[GlobalClass]
public partial class GameDialoguesConfiguration : Resource
{
    [Export] public DialogueConfiguration? Test;
}