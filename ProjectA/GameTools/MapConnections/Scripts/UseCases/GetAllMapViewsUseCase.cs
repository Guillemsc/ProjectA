using System;
using System.Collections.Generic;
using System.IO;
using Game.GameContext.Maps.Views;
using Godot;
using GUtils.DiscriminatedUnions;
using GUtils.Optionals;
using GUtils.Types;
using GUtilsGodot.Extensions;

namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class GetAllMapViewsUseCase
{
    public IEnumerable<Tuple<PackedScene, MapView>> Execute()
    {
        IEnumerable<FileInfo> assets = GodotFileSystemExtensions.GetAllAssetsAtPathWithExtension(
            "res://Game/",
            ".tscn"
        );
        
        foreach (FileInfo fileInfo in assets)
        {
            OneOf<PackedScene, ErrorMessage> loadResult = ResourceLoaderExtensions.Load<PackedScene>(fileInfo.FullName);

            if (!loadResult.HasFirst)
            {
                continue;
            }

            PackedScene packedScene = loadResult.UnsafeGetFirst();

            Optional<MapView> optionalMapView = packedScene.SafeInstantiate<MapView>(PackedScene.GenEditState.Main);

            bool hasValue = optionalMapView.TryGet(out MapView mapView);

            if (!hasValue)
            {
                continue;
            }

            yield return new Tuple<PackedScene, MapView>(packedScene, mapView);
        }
    }
}