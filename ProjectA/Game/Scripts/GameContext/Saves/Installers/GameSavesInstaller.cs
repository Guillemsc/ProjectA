using Game.GameContext.General.Configurations;
using Game.GameContext.Saves.Datas;
using Game.GameContext.Saves.UseCases;
using Game.ServicesContext.Saves.Services;
using GUtils.Di.Builder;

namespace Game.GameContext.Saves.Installers;

public static class GameSavesInstaller
{
    public static void InstallGameSaves(this IDiContainerBuilder builder)
    {
        builder.Bind<SavesData>().FromNew();
        
        builder.Bind<LoadGameMapSaveDataUseCase>()
            .FromFunction(c => new LoadGameMapSaveDataUseCase(
                c.Resolve<GameApplicationContextConfiguration>(),
                c.Resolve<IGameSavesService>(),
                c.Resolve<SavesData>()
            ));

        builder.Bind<SaveGameMapSaveDataUseCase>()
            .FromFunction(c => new SaveGameMapSaveDataUseCase(
                c.Resolve<IGameSavesService>()
            ));
    }
}