using System.Collections.Generic;
using Beltv1C.Buildings.Enums;
using Godot;

namespace Beltv1C.Buildings.Scripts;

public partial class BuildingManager : Node2D
{
	public static BuildingManager Instance { get; private set; }

	private bool isPlacing = false;
    private BuildingType currentBuildingType;
    private Node2D currentBuilding;
    private Node tempBuildings;
	private TileMapLayer tileMapLayer;


	[Signal]
	public delegate void BuildingTypeSignalEventHandler(int buildingType);

	private Dictionary<BuildingType, PackedScene> buildingScenePaths = new()
    {
        { BuildingType.Generator, GD.Load<PackedScene>("res://Buildings/Scenes/generator.tscn") },
    };

	public override void _Ready()
	{
		if (Instance == null)
			Instance = this;
		else
			GD.PrintErr("BuildingManager already exists.");

        BuildingTypeSignal += (int buildingType) => StartBuildingPlacement(buildingType);

		tempBuildings = GetNode<Node>("TempBuildings");
		tileMapLayer = GetNode<TileMapLayer>("TileMapLayer");
	}

	public void StartBuildingPlacement(int buildingType)
	{
		GD.Print($"Building type selected: {buildingType}");
		isPlacing = true;
        currentBuildingType = (BuildingType)buildingType;
        
        // Instantiate the building
        currentBuilding = (Node2D)buildingScenePaths[currentBuildingType].Instantiate();
        
        // Add the building to the tempBuildings node
        tempBuildings.AddChild(currentBuilding);
	}

	public override void _Process(double delta)
	{
		if (isPlacing && currentBuilding != null)
        {
            // Get the global mouse position and snap to the tilemap grid
            Vector2 mousePosition = GetGlobalMousePosition();
			Vector2I tilePosition = tileMapLayer.LocalToMap(mousePosition);
			Vector2 snappedPosition = tileMapLayer.MapToLocal(tilePosition);
            
            // Set the building's position
            currentBuilding.GlobalPosition = snappedPosition;
        }
	}
}
