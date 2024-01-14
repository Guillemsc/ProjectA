using GUtils.Executables;
using GUtilsGodot.Quitting.Services;

namespace Game.MetaContext.MainMenuUi.UseCases;

public sealed class WhenQuitButtonPressedUseCase : IExecutable
{
    readonly IQuitService _quitService;

    public WhenQuitButtonPressedUseCase(IQuitService quitService)
    {
        _quitService = quitService;
    }

    public void Execute()
    {
        _quitService.Quit();
    }
}