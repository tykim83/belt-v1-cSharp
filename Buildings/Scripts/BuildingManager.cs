using System.Collections.Generic;
using Beltv1C.Buildings.Constants;
using Beltv1C.Buildings.Enums;
using Godot;

namespace Beltv1C.Buildings.Scripts;

public partial class BuildingManager : Node2D
{
	public static BuildingManager Instance { get; private set; }
	private TileMapLayer tileMapLayer;
    private Node tempBuildings;
	private Node buildings;

	private Dictionary<Vector2I, BaseBuilding> buildingMap = new();
	private bool isPlacing = false;
    private BuildingType currentBuildingType;
    private BaseBuilding currentBuilding;

	[Signal]
	public delegate void BuildingTypeSignalEventHandler(int buildingType);

	public override void _Input(InputEvent @event)
    {
        if (isPlacing && Input.IsActionJustPressed("rotate"))
        {
			//TODO: Check If Corner is needed for Belt

			currentBuilding.RotateBuilding(false);
        }
		else if (isPlacing && Input.IsActionJustPressed("click"))
		{
			Vector2 mousePosition = GetGlobalMousePosition();
			Vector2I tilePosition = tileMapLayer.LocalToMap(mousePosition);
			currentBuilding.GetParent().RemoveChild(currentBuilding);
			buildings.AddChild(currentBuilding);
			buildingMap[tilePosition] = currentBuilding;

			currentBuilding.Create();
			//Todo: Add connection to adjacent buildings

			isPlacing = false;
			currentBuilding = null;
		}
    }



	public void StartBuildingPlacement(int buildingType)
	{
		GD.Print($"Building type selected: {buildingType}");
		isPlacing = true;
        currentBuildingType = (BuildingType)buildingType;
        
        // Instantiate the building
        currentBuilding = (BaseBuilding)BuildingConstants.BuildingScenePaths[currentBuildingType].Instantiate();
        
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

	public override void _Ready()
	{
		if (Instance == null)
			Instance = this;
		else
			GD.PrintErr("BuildingManager already exists.");

        BuildingTypeSignal += (int buildingType) => StartBuildingPlacement(buildingType);

		tempBuildings = GetNode<Node>("TempBuildings");
		buildings = GetNode<Node>("Buildings");
		tileMapLayer = GetNode<TileMapLayer>("TileMapLayer");
	}
}
