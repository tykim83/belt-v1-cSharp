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
		{ BuildingType.Belt, GD.Load<PackedScene>("res://Buildings/Scenes/belt.tscn") },
		{ BuildingType.Factory, GD.Load<PackedScene>("res://Buildings/Scenes/factory.tscn") }
    };

	public static readonly Dictionary<string, Texture2D> BeltTexture = new()
    {
        { "Left_Right", (Texture2D)GD.Load("res://Art/Conveyors/conveyors_Left_Right.png") },
        { "Right_Left", (Texture2D)GD.Load("res://Art/Conveyors/conveyors_Right_Left.png") },
        { "Up_Down", (Texture2D)GD.Load("res://Art/Conveyors/conveyors_Up_Down.png") },
        { "Down_Up", (Texture2D)GD.Load("res://Art/Conveyors/conveyors_Down_Up.png") },

        // Corners
        { "Left_Down", (Texture2D)GD.Load("res://Art/Conveyors/conveyors_Left_Down.png") },
        { "Down_Left", (Texture2D)GD.Load("res://Art/Conveyors/conveyors_Down_Left.png") },
        { "Left_Up", (Texture2D)GD.Load("res://Art/Conveyors/conveyors_Left_Up.png") },
        { "Up_Left", (Texture2D)GD.Load("res://Art/Conveyors/conveyors_Up_Left.png") },
        { "Right_Down", (Texture2D)GD.Load("res://Art/Conveyors/conveyors_Right_Down.png") },
        { "Down_Right", (Texture2D)GD.Load("res://Art/Conveyors/conveyors_Down_Right.png") },
        { "Right_Up", (Texture2D)GD.Load("res://Art/Conveyors/conveyors_Right_Up.png") },
        { "Up_Right", (Texture2D)GD.Load("res://Art/Conveyors/conveyors_Up_Right.png") }
    };

	public const double MOVEMENT_SPEED = 64.0;
	public const float BUFFER_ZONE = 0.5f;
}
