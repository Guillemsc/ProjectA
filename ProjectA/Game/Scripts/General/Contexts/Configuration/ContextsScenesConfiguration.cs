using Godot;

namespace Game.General.Contexts.Configuration;

[GlobalClass]
public partial class ContextsScenesConfiguration : Resource
{
    [Export] public PackedScene? ServicesContextPrefab;
    [Export] public PackedScene? LoadingScreenContextPrefab;
    [Export] public PackedScene? MetaContextPrefab;
    [Export] public PackedScene? GameContextPrefab;
}