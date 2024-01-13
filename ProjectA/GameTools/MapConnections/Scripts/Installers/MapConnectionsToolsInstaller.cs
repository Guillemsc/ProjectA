using Game.GameContext.Maps.Configurations;
using GameTools.MapConnections.Scripts.Configurations;
using GameTools.MapConnections.Scripts.Datas;
using GameTools.MapConnections.Scripts.UseCases;
using Godot;
using GUtils.Di.Builder;
using GUtils.Tasks.Extensions;
using GUtils.Tasks.Runners;
using GUtilsGodot.Di.Contexts;
using GUtilsGodot.Extensions;

public partial class MapConnectionsToolsInstaller : AutoInstallControlDiContext
{
	[Export] public GraphEdit? GraphEdit;
	[Export] public Button? RefreshMapListButton;
	[Export] public Button? RefreshMapNodesButton;
	[Export] public Button? SaveStateButton;
	[Export] public PackedScene? NodeViewPrefab;
	[Export] public MapConnectionsToolNodeConfiguration? MapConnectionsToolNodeConfiguration;
	[Export] public GameMapsConfiguration? GameMapsConfiguration;
	
	public override void Install(IDiContainerBuilder builder)
	{
		builder.Bind<MapNodesData>().FromNew();
		
		builder.InstallAsyncTaskRunner();
		
		builder.Bind<LinkGraphEditUseCase>()
			.FromFunction(c => new LinkGraphEditUseCase(
				GraphEdit!,
				c.Resolve<WhenGraphEditConnectionRequestUseCase>(),
				c.Resolve<WhenGraphEditDisconnectionRequestUseCase>()
			))
			.WhenInit(o => o.Execute)
			.NonLazy();
		
		builder.Bind<WhenGraphEditConnectionRequestUseCase>()
			.FromFunction(c => new WhenGraphEditConnectionRequestUseCase(
				GraphEdit!,
				c.Resolve<GetNodeByNameUseCase>(),
				c.Resolve<ConnectMapsUseCase>()
			));

		builder.Bind<WhenGraphEditDisconnectionRequestUseCase>()
			.FromFunction(c => new WhenGraphEditDisconnectionRequestUseCase(
				GraphEdit!,
				c.Resolve<GetNodeByNameUseCase>(),
				c.Resolve<DisconnectMapsUseCase>()
			));
		
		builder.Bind<WhenRefreshMapListButtonPressedUseCase>()
			.FromFunction(c => new WhenRefreshMapListButtonPressedUseCase(
				c.Resolve<RefreshMapsListUseCase>()
			))
			.LinkButtonPressed(RefreshMapListButton!);

		builder.Bind<WhenRefreshMapNodesButtonPressedUseCase>()
			.FromFunction(c => new WhenRefreshMapNodesButtonPressedUseCase(
				c.Resolve<RefreshMapNodesUseCase>()
			))
			.LinkButtonPressed(RefreshMapNodesButton!);

		builder.Bind<WhenSaveStateButtonUseCase>()
			.FromFunction(c => new WhenSaveStateButtonUseCase(
				c.Resolve<SaveNodesStateUseCase>()
			))
			.LinkButtonPressed(SaveStateButton!);

		builder.Bind<GetAllMapViewsUseCase>()
			.FromFunction(c => new GetAllMapViewsUseCase(
			));

		builder.Bind<RefreshMapsListUseCase>()
			.FromFunction(c => new RefreshMapsListUseCase(
				c.Resolve<IAsyncTaskRunner>(),
				GameMapsConfiguration!,
				c.Resolve<GetAllMapViewsUseCase>()
			));

		builder.Bind<RefreshMapNodesUseCase>()
			.FromFunction(c => new RefreshMapNodesUseCase(
				GameMapsConfiguration!,
				c.Resolve<MapNodesData>(),
				c.Resolve<AddNodeUseCase>()
			));

		builder.Bind<CheckThatAllConnectionsHaveUidUseCase>()
			.FromFunction(c => new CheckThatAllConnectionsHaveUidUseCase(
				c.Resolve<MapNodesData>()
			));
		
		builder.Bind<GetNodeByNameUseCase>()
			.FromFunction(c => new GetNodeByNameUseCase(
				c.Resolve<MapNodesData>()
			));

		builder.Bind<GetMapConnectionForConnectionTypeWithIndexUseCase>()
			.FromFunction(c => new GetMapConnectionForConnectionTypeWithIndexUseCase());

		builder.Bind<ConnectMapsUseCase>()
			.FromFunction(c => new ConnectMapsUseCase(
				c.Resolve<GetMapConnectionForConnectionTypeWithIndexUseCase>()
			));

		builder.Bind<DisconnectMapsUseCase>()
			.FromFunction(c => new DisconnectMapsUseCase(
				c.Resolve<GetMapConnectionForConnectionTypeWithIndexUseCase>()
			));

		builder.Bind<GetNodeFromMapUseCase>()
			.FromFunction(c => new GetNodeFromMapUseCase(
				c.Resolve<MapNodesData>()
			));
		
		builder.Bind<LoadNodesConnections>()
			.FromFunction(c => new LoadNodesConnections(
				c.Resolve<MapNodesData>(),
				GraphEdit!,
				c.Resolve<GetNodeFromMapUseCase>()
			));
		
		builder.Bind<SaveNodesStateUseCase>()
			.FromFunction(c => new SaveNodesStateUseCase(
				c.Resolve<MapNodesData>(),
				MapConnectionsToolNodeConfiguration!
			));

		builder.Bind<LoadNodesStateUseCase>()
			.FromFunction(c => new LoadNodesStateUseCase(
				MapConnectionsToolNodeConfiguration!,
				c.Resolve<MapNodesData>()
			));
		
		builder.Bind<AddNodeUseCase>()
			.FromFunction(c => new AddNodeUseCase(
				GraphEdit!,
				NodeViewPrefab!,
				c.Resolve<MapNodesData>()
			));

		builder.Bind<LoadUseCase>()
			.FromFunction(c => new LoadUseCase(
				c.Resolve<RefreshMapNodesUseCase>(),
				c.Resolve<CheckThatAllConnectionsHaveUidUseCase>(),
				c.Resolve<LoadNodesStateUseCase>(),
				c.Resolve<LoadNodesConnections>()
			))
			.WhenInit(o => o.Execute)
			.NonLazy();
	}
}
