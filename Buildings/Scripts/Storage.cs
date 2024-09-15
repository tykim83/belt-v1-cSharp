using Beltv1C.Buildings.Enums;
using Godot;

namespace Beltv1C.Buildings.Scripts;

public partial class Storage : BaseBuilding
{
    private int itemCount = 0;

	public override void _Ready()
    {
        directionFrom = Direction.Right | Direction.Left | Direction.Up | Direction.Down;
        directionTo = Direction.None;
		buildingType = BuildingType.Storage;

        // Get the references to the nodes in the scene
        item = GetNode<Node2D>("Item");
        buildingSprite = GetNode<Sprite2D>("BuildingSprite");
        buildingArrow = GetNode<Sprite2D>("BuildingArrow");

        Color currentColor = buildingSprite.Modulate;
        currentColor.A = 0.5f; 
        buildingSprite.Modulate = currentColor;


        // Optional: Add error checking to make sure nodes were found
        if (item == null)
            GD.PrintErr("Item node not found!");

        if (buildingSprite == null)
            GD.PrintErr("ItemGive node (generatorSprite) not found!");

        if (buildingArrow == null)
            GD.PrintErr("Arrow node not found!");
    }

	public override void RotateBuilding(bool needsCorner) 
    {
        buildingSprite.RotationDegrees += 90;
		directionFrom = directionFrom.RotateClockwise();
        directionTo = directionTo.RotateClockwise();
    }

    public override void ItemReceived()
    {
        GD.Print(inputItem.ItemType + " received!");
        inputItem.QueueFree();
        inputItem = null;
        itemCount++;

        GD.Print("Item received! Total items: " + itemCount);
        base.ItemReceived();
    }
}
