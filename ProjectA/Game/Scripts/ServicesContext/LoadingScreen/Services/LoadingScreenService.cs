using Game.ServicesContext.LoadingScreen.Enums;

namespace Game.ServicesContext.LoadingScreen.Services;

public sealed class LoadingScreenService : ILoadingScreenService
{
    public LoadingScreenType LoadingScreenToUse { get; set; } = LoadingScreenType.Default;
}