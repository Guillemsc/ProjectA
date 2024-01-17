using System;
using System.Collections.Generic;

namespace Game.GameContext.Saves.Datas;

public sealed class GameSaveData
{
    public bool Used;
    
    public string Map = string.Empty;
    public Guid ConnectionUid;
    
    public TimeSpan GameTime = TimeSpan.Zero;

    public Dictionary<string, GameMapSaveData> MapSaveDatas = new();
}