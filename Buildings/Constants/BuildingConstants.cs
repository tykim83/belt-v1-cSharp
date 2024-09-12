using System.Collections.Generic;
using Beltv1C.Buildings.Enums;
using Godot;

namespace Beltv1C.Buildings.Constants;

public static class BuildingConstants
{
	public static readonly Dictionary<BuildingType, PackedScene> BuildingScenePaths = new()
    {
        { BuildingType.Generator, GD.Load<PackedScene>("res://Buildings/Scenes/generator.tscn") },
    };
}
