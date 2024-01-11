using Game.GameContext.Entities.Services;
using Godot;
using GUtils.Services.Locators;

namespace Game.GameContext.Entities.Nodes;

public partial class RegisterEntityNode : Node2D
{
    [Export] public Node2D? ToRegister;
    
    public override void _Ready()
    {
        IGameEntitiesService gameEntitiesService = ServiceLocator.Get<IGameEntitiesService>();
        gameEntitiesService.Register(ToRegister!);
    }
}