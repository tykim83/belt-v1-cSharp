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

			// Skip if the mouse is over the main panel
			var mainPanel = GetTree().Root.GetNode<Control>("Main/UI/Panel"); 
			if (mainPanel.GetRect().HasPoint(mousePosition))
				return;

			Vector2I tilePosition = tileMapLayer.LocalToMap(mousePosition);
			currentBuilding.GetParent().RemoveChild(currentBuilding);
			buildings.AddChild(currentBuilding);
			buildingMap[tilePosition] = currentBuilding;

			currentBuilding.Create();
			AddConnections();

			isPlacing = false;
			currentBuilding = null;
		}
    }

	public void AddConnections()
	{
		var adjacentBuildings = GetAdjacentBuildings();

		switch (currentBuildingType)
		{
			// Only Send 
			case BuildingType.Generator:
				if (adjacentBuildings.ContainsKey(currentBuilding.DirectionTo))
				{
					var buildingTo = adjacentBuildings[currentBuilding.DirectionTo];

					// Don't connect if the belt is coming to your direction
					if (buildingTo.BuildingType == BuildingType.Belt && currentBuilding.DirectionTo == buildingTo.DirectionTo.GetOppositeDirection())
						return;

					// Don't connect if building doesn't accept from any direction
					if (buildingTo.DirectionFrom == Direction.None)
						return;

					// Set the next building in line
					currentBuilding.SetNext(adjacentBuildings[currentBuilding.DirectionTo]);
				}
				break;

			// Receive from multiple directions
			case BuildingType.Storage:
				foreach (var direction in new Direction[] { Direction.Left, Direction.Right, Direction.Up, Direction.Down })
				{
					if (adjacentBuildings.ContainsKey(direction))
					{
						var adjacentBuilding = adjacentBuildings[direction];

						// Check if the adjacent building is directing towards the Storage
						if (adjacentBuilding.DirectionTo == direction.GetOppositeDirection())
						{
							adjacentBuilding.SetNext(currentBuilding);
						}
					}
				}
				break;
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

	private Dictionary<Direction, BaseBuilding> GetAdjacentBuildings()
    {
        Vector2 mousePosition = GetGlobalMousePosition();
        Vector2I tilePosition = tileMapLayer.LocalToMap(mousePosition);

        // Create a dictionary to store adjacent buildings
        Dictionary<Direction, BaseBuilding> adjacentBuildings = new();

        // Check for the left building
        BaseBuilding leftBuilding = buildingMap.GetValueOrDefault(tilePosition + new Vector2I(-1, 0), null);
        if (leftBuilding != null)
			adjacentBuildings[Direction.Left] = leftBuilding;

        // Check for the right building
        BaseBuilding rightBuilding = buildingMap.GetValueOrDefault(tilePosition + new Vector2I(1, 0), null);
        if (rightBuilding != null)
			adjacentBuildings[Direction.Right] = rightBuilding;

        // Check for the top building
        BaseBuilding topBuilding = buildingMap.GetValueOrDefault(tilePosition + new Vector2I(0, -1), null);
        if (topBuilding != null)
			adjacentBuildings[Direction.Up] = topBuilding;

        // Check for the bottom building
        BaseBuilding bottomBuilding = buildingMap.GetValueOrDefault(tilePosition + new Vector2I(0, 1), null);
        if (bottomBuilding != null)
			adjacentBuildings[Direction.Down] = bottomBuilding;

        // Return the dictionary containing adjacent buildings
        return adjacentBuildings;
    }
}
