using Game.GameContext.General.Configurations;
using Game.GameContext.Saves.Datas;
using Game.ServicesContext.Saves.Services;
using GUtils.Optionals;

namespace Game.GameContext.Saves.UseCases;

public sealed class LoadGameMapSaveDataUseCase
{
    readonly GameApplicationContextConfiguration _gameApplicationContextConfiguration;
    readonly IGameSavesService _gameSavesService;
    readonly SavesData _savesData;

    public LoadGameMapSaveDataUseCase(
        GameApplicationContextConfiguration gameApplicationContextConfiguration,
        IGameSavesService gameSavesService, 
        SavesData savesData
        )
    {
        _gameApplicationContextConfiguration = gameApplicationContextConfiguration;
        _gameSavesService = gameSavesService;
        _savesData = savesData;
    }

    public void Execute()
    {
        Optional<GameSaveData> optionalGameSaveData = _gameSavesService.GetCurrentSaveData();

        bool hasGameSaveData = optionalGameSaveData.TryGet(out GameSaveData gameSaveData);

        if (!hasGameSaveData)
        {
            return;
        }

        bool mapSaveDataFound = gameSaveData.MapSaveDatas.TryGetValue(
            _gameApplicationContextConfiguration.MapToLoad,
            out GameMapSaveData? gameMapSaveData
        );

        if (!mapSaveDataFound)
        {
            gameMapSaveData = new GameMapSaveData();
            gameSaveData.MapSaveDatas.Add(_gameApplicationContextConfiguration.MapToLoad, gameMapSaveData);
        }

        _savesData.CurrentMapSaveData = gameMapSaveData!;
    }
}