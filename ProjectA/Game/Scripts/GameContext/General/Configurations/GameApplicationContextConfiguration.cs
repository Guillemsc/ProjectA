using GUtils.Directions;

namespace Game.GameContext.General.Configurations;

public sealed class GameApplicationContextConfiguration
{
    public string MapToLoad { get; }
    public string SpawnId { get; }
    public bool PlayerAppears { get; }
    public HorizontalDirection PlayerDirection { get; }
    
    public GameApplicationContextConfiguration(
        string mapToLoad, 
        string spawnId, 
        bool playerAppears, 
        HorizontalDirection playerDirection
        )
    {
        MapToLoad = mapToLoad;
        SpawnId = spawnId;
        PlayerAppears = playerAppears;
        PlayerDirection = playerDirection;
    }
}