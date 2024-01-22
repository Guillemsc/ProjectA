using GDebugPanelGodot.Constants;
using GDebugPanelGodot.Views;

namespace GDebugPanelGodot.UseCases;

public static class RefreshSectionViewNameUseCase
{
    public static void Execute(DebugPanelSectionView sectionView)
    {
        string arrowChar = sectionView.Section!.Collapsed
            ? CharactersConstants.MenuFoldedChar
            : CharactersConstants.MenuUnfoldedChar;

        sectionView.SectionLabel!.Text = $"{arrowChar} {sectionView.Section!.Name}";
    }
}