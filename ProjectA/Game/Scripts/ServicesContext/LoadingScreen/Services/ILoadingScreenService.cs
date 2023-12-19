using Game.ServicesContext.LoadingScreen.Enums;

namespace Game.ServicesContext.LoadingScreen.Services;

public interface ILoadingScreenService
{
    LoadingScreenType LoadingScreenToUse { get; set; }
}