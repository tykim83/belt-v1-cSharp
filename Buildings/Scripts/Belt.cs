using Beltv1C.Buildings.Enums;
using Beltv1C.Items.Enums;
using Godot;

namespace Beltv1C.Buildings.Scripts;

public partial class Belt : BaseBuilding
{
    private ItemType itemType;

    public override void _Ready()
    {
		itemType = ItemType.Wood;
        directionFrom = Direction.Left;
        directionTo = Direction.Right;
        buildingType = BuildingType.Belt;

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
        directionTo = directionTo.RotateClockwise();
    }
}
