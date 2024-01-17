using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Saves.Datas;
using GUtils.Extensions;
using GUtils.Logging.Loggers;
using GUtils.Optionals;
using GUtils.Persistence.Serialization;
using GUtilsGodot.Logging.Outputs;
using GUtilsGodot.Persistence.StorageMethods;

namespace Game.ServicesContext.Saves.Services;

public sealed class GameSavesService : IGameSavesService
{
    public const int SaveDataSlots = 3;
    
    readonly List<SerializableData<GameSaveData>> _saveDatas = new();
    
    Optional<SerializableData<GameSaveData>> _currentSaveData;
    
    public GameSavesService()
    {
        for (int i = 0; i < SaveDataSlots; i++)
        {
            SerializableData<GameSaveData> serializableData = new(
                GodotFilePersistenceStorageMethod.Instance,
                $"Game.ServicesContext.Saves.Services.GameSavesService.SaveDataSlot{i}",
                () => new GameSaveData(),
                new ConditionalLogger(() => true, GodotLogOutput.Instance)
            );
            
            _saveDatas.Add(serializableData);
        }
    }

    public async Task Load(CancellationToken cancellationToken)
    {
        foreach (SerializableData<GameSaveData> serializableGameSaveData in _saveDatas)
        {
            await serializableGameSaveData.Load(cancellationToken);
        }
        
        SetCurrentSaveDataSlot(0);
    }

    public void SetCurrentSaveDataSlot(int slot)
    {
        bool found = _saveDatas.TryGet(slot, out SerializableData<GameSaveData> gameSaveData);

        if (!found)
        {
            return;
        }

        _currentSaveData = gameSaveData;
    }

    public Optional<GameSaveData> GetCurrentSaveData()
    {
        return _currentSaveData.Map(o => o.Data);
    }

    public Task SaveCurrentSaveData(CancellationToken cancellationToken)
    {
        bool hasCurrentSaveData = _currentSaveData.TryGet(out SerializableData<GameSaveData> saveData);

        if (!hasCurrentSaveData)
        {
            return Task.CompletedTask;
        }

        return saveData.Save(cancellationToken);
    }
}