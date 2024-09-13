using System.Collections.Generic;
using Beltv1C.Buildings.Enums;
using Godot;

namespace Beltv1C.Buildings.Constants;

public static class BuildingConstants
{
	public static readonly Dictionary<BuildingType, PackedScene> BuildingScenePaths = new()
    {
        { BuildingType.Generator, GD.Load<PackedScene>("res://Buildings/Scenes/generator.tscn") },
		{ BuildingType.Storage, GD.Load<PackedScene>("res://Buildings/Scenes/storage.tscn") },
    };

	public const double MOVEMENT_SPEED = 64.0;
	public const float BUFFER_ZONE = 0.5f;
}
