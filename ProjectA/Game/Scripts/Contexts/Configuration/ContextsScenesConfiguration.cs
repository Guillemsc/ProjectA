using Godot;

namespace Game.Contexts.Configuration;

[GlobalClass]
public partial class ContextsScenesConfiguration : Resource
{
    [Export] public PackedScene ServicesContextPrefab;
    [Export] public PackedScene GameContextPrefab;
}