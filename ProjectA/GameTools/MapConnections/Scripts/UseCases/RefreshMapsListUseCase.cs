using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Maps.Configurations;
using Game.GameContext.Maps.Views;
using Godot;
using GUtils.Tasks.Runners;
using GUtilsGodot.Extensions;
using TaskExtensions = GUtilsGodot.Extensions.TaskExtensions;

namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class RefreshMapsListUseCase
{
    readonly IAsyncTaskRunner _asyncTaskRunner;
    readonly GameMapsConfiguration _gameMapsConfiguration;
    readonly GetAllMapViewsUseCase _getAllMapViewsUseCase;

    public RefreshMapsListUseCase(
        IAsyncTaskRunner asyncTaskRunner,
        GameMapsConfiguration gameMapsConfiguration,
        GetAllMapViewsUseCase getAllMapViewsUseCase)
    {
        _asyncTaskRunner = asyncTaskRunner;
        _gameMapsConfiguration = gameMapsConfiguration;
        _getAllMapViewsUseCase = getAllMapViewsUseCase;
    }

    public void Execute()
    {
        async Task Run(CancellationToken cancellationToken)
        {
            List<MapConfiguration> toCheck = new();
            toCheck.AddRange(_gameMapsConfiguration.MapConfigurations);
            
            IEnumerable<Tuple<PackedScene, MapView>> mapViews = _getAllMapViewsUseCase.Execute();
        
            foreach (Tuple<PackedScene, MapView> item in mapViews)
            {
                await TaskExtensions.GodotYield();
                await TaskExtensions.GodotYield();

                MapConfiguration? foundMapConfiguration = toCheck.Find(i => i.Map!.Equals(item.Item1.ResourcePath));

                if (foundMapConfiguration != null)
                {
                    toCheck.Remove(foundMapConfiguration);
                    continue;
                }

                MapConfiguration mapConfiguration = new();
                mapConfiguration.Map = item.Item1.ResourcePath;
                
                _gameMapsConfiguration.MapConfigurations!.Add(mapConfiguration);
                
                GD.Print($"Added map {item.Item1.ResourcePath}");
            }

            foreach (MapConfiguration remove in toCheck)
            {
                _gameMapsConfiguration.MapConfigurations!.Remove(remove);
                
                GD.Print($"Removed map {remove.Map}");
            }
            
            _gameMapsConfiguration.Save();
        }

        _asyncTaskRunner.Run(Run);
    }
}