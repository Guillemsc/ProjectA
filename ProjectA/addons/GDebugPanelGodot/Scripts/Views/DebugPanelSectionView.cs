using Godot;

namespace GDebugPanelGodot.Views;

public partial class DebugPanelSectionView : Control
{
    [Export] public Label? NameLabel;
    [Export] public Control? ContentParent;
}