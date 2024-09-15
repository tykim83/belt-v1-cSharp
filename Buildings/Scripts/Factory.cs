
using Beltv1C.Buildings.Enums;
using Beltv1C.Items.Constants;
using Beltv1C.Items.Enums;
using Beltv1C.Items.Scripts;
using Godot;

namespace Beltv1C.Buildings.Scripts;

public partial class Factory : BaseBuilding
{
	private Timer timer;

    public override void _Ready()
    {
        directionFrom = Direction.Left;
        directionTo = Direction.Right;
        buildingType = BuildingType.Factory;

        // Get the references to the nodes in the scene
        item = GetNode<Node2D>("Item");
        buildingSprite = GetNode<Sprite2D>("BuildingSprite");
        buildingArrow = GetNode<Sprite2D>("BuildingArrow");
		timer = GetNode<Timer>("Timer");
		timer.Timeout += OnTimerTimeout;

        Color currentColor = buildingSprite.Modulate;
        currentColor.A = 0.5f; 
        buildingSprite.Modulate = currentColor;

        // Optional: Add error checking to make sure nodes were found
        if (item == null)
            GD.PrintErr("Item node not found!");
		
        if (timer == null)
            GD.PrintErr("Timer node not found!");

        if (buildingSprite == null)
            GD.PrintErr("ItemGive node (generatorSprite) not found!");

        if (buildingArrow == null)
            GD.PrintErr("Arrow node not found!");
    }

	public override void RotateBuilding(bool needsCorner) 
    {
		buildingSprite.RotationDegrees += 90;
        directionFrom = directionFrom.RotateClockwise();
		directionTo = directionFrom.GetOppositeDirection();
    }

	public override void ItemReceived() 
    {
        inputItem.QueueFree();
		timer.Start();
    }

	private void OnTimerTimeout()
    {
        timer.Stop();

		hasArrived = true;

        inputItem = (BaseItem)ItemConstants.BaseItemScene.Instantiate();

        // Set the position relative to the item node
        inputItem.GlobalPosition = Position - item.GlobalPosition;

        // Set the ItemType (e.g., to Wood)
        inputItem.Create(ItemType.Planks);

        // Add the inputItem to the item node
        item.AddChild(inputItem);

        hasArrived = true;
    }
}
