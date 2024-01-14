using Game.ServicesContext.LoadingScreen.Services;
using Game.ServicesContext.Music.Services;
using Game.ServicesContext.Time.Services;
using Godot;
using GUtils.ApplicationContexts.Services;
using GUtils.Di.Builder;
using GUtils.Loading.Services;
using GUtils.Services.Extensions;
using GUtils.Tick.Services;
using GUtilsGodot.Cameras.Services;
using GUtilsGodot.Di.Installers;
using GUtilsGodot.Quitting.Services;
using GUtilsGodot.Roots.Services;
using GUtilsGodot.Tick.Services;
using GUtilsGodot.Time.TimeContexts;
using GUtilsGodot.UiFrame.Services;
using GUtilsGodot.UiStack.Services;

namespace Game.ServicesContext.General.Installers;

public partial class ServicesContextNodeInstaller : NodeInstaller
{
    [Export] public TickablesServiceNode? TickablesService;
    [Export] public GodotTimeContext? TimeContext;
    [Export] public GodotPhysicsTimeContext? PhysicsTimeContext;
    [Export] public MusicService? MusicService;
    [Export] public Cameras2dServiceNode? Cameras2dService;
    [Export] public UiFrameService? UiFrameService;
    [Export] public QuitServiceNode? QuitService;
    
    public override void Install(IDiContainerBuilder builder)
    {
        builder.Bind<ILoadingService>()
            .FromFunction(c => new LoadingService())
            .LinkToServiceLocator();

        builder.Bind<IApplicationContextService>()
            .FromFunction(c => new ApplicationContextService())
            .LinkToServiceLocator();

        builder.Bind<ITickablesService>()
            .FromInstance(TickablesService!)
            .LinkToServiceLocator();
        
        builder.Bind<IGameTimesService>()
            .FromInstance(new GameTimesService(TimeContext!, PhysicsTimeContext!))
            .LinkToServiceLocator();
        
        builder.Bind<ICameras2dService>()
            .FromInstance(Cameras2dService!)
            .LinkToServiceLocator();
        
        builder.Bind<IUiFrameService>()
            .FromInstance(UiFrameService!)
            .LinkToServiceLocator();;

        builder.Bind<IUiStackService>()
            .FromFunction(c => new UiStackService(
                c.Resolve<IUiFrameService>()
            ))
            .LinkToServiceLocator();
        
        builder.Bind<IRootNodeService>()
            .FromFunction(c => new RootNodeService(GetParent()))
            .LinkToServiceLocator();

        builder.Bind<ILoadingScreenService>()
            .FromFunction(c => new LoadingScreenService())
            .LinkToServiceLocator();

        builder.Bind<IMusicService>()
            .FromInstance(MusicService!)
            .LinkToServiceLocator();

        builder.Bind<IQuitService>()
            .FromInstance(QuitService!)
            .LinkToServiceLocator();
    }
}