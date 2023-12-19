namespace Game.GameContext.General.Configurations;

public sealed class GameApplicationContextConfiguration
{
    public string MapToLoad { get; }
    public string SpawnId { get; }
    public bool PlayerAppears { get; }
    
    public GameApplicationContextConfiguration(string mapToLoad, string spawnId, bool playerAppears)
    {
        MapToLoad = mapToLoad;
        SpawnId = spawnId;
        PlayerAppears = playerAppears;
    }
}