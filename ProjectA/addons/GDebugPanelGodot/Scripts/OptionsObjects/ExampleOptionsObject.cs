using Godot;

namespace GDebugPanelGodot.OptionsObjects;

public sealed class ExampleOptionsObject
{
    public void ButtonExample() => GD.Print("Button Option!");
    public bool ToggleExample { get; set; }
    public string DynamicInfoExample { get; set; } = "Dynamic Info";
    public string InfoExample => "Info";
    public int IntExample { get; set; }
    public float FloatExample { get; set; }
}