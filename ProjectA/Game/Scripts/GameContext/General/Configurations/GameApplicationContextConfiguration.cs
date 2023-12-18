namespace Game.GameContext.General.Configurations;

public sealed class GameApplicationContextConfiguration
{
    public string MapToLoad { get; }
    public string SpawnId { get; }
    
    public GameApplicationContextConfiguration(string mapToLoad, string spawnId)
    {
        MapToLoad = mapToLoad;
        SpawnId = spawnId;
    }
}