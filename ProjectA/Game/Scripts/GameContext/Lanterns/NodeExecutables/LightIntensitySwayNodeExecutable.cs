using Godot;
using GTweensGodot.Extensions;
using GUtilsGodot.NodeExecutables.Nodes;

namespace Game.GameContext.Lanterns.NodeExecutables;

public partial class LightIntensitySwayNodeExecutable : NodeExecutable
{
    [Export] public Light2D? Light2D;
    [Export] public float Duration;
    [Export] public float Strenght;
    [Export] public Curve? Curve;
    
    public override void Execute()
    {
        float randomSimulation = (float)GD.RandRange(0, Duration);
        
        Light2D!.TweenEnergy(Light2D!.Energy + Strenght, Duration)
            .SetEasing(Curve!)
            .SetMaxLoops()
            .Simulate(randomSimulation)
            .Play();
    }
}