using Game.GameContext.Dialogues.Enums;
using Godot;

namespace Game.GameContext.Dialogues.Configurations;

[GlobalClass]
public partial class DialogueSpeakerConfiguration : Resource
{
    [Export] public DialogueSpeaker Speaker;
    [Export] public Texture2D? PortraitTexture;
}