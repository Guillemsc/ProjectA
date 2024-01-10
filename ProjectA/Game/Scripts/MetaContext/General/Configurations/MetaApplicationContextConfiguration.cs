namespace Game.MetaContext.General.Configurations;

public sealed class MetaApplicationContextConfiguration
{
    public bool PlayIntro { get; }
    
    public MetaApplicationContextConfiguration(bool playIntro)
    {
        PlayIntro = playIntro;
    }
}