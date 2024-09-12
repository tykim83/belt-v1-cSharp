using Beltv1C.Buildings.Enums;
using Beltv1C.Items.Constants;
using Beltv1C.Items.Enums;
using Beltv1C.Items.Scripts;
using Godot;
using System;

namespace Beltv1C.Buildings.Scripts;

public partial class Generator : BaseBuilding
{
    private Timer timer;

    public override void _Ready()
    {
		itemType = ItemType.Wood;
        directionFrom = Direction.Right;
        directionTo = Direction.None;

        // Get the references to the nodes in the scene
        item = GetNode<Node2D>("Item");
        buildingSprite = GetNode<Sprite2D>("BuildingSprite");
        buildingArrow = GetNode<Sprite2D>("BuildingArrow");
		timer = GetNode<Timer>("Timer");

		timer.Timeout += OnTimerTimeout;

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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void Create()
    {
        timer.Start();
    }

    public override void SetNext(Node2D receivedNext)
    {
        throw new NotImplementedException();
    }

    public override void MoveBoxToNext(float delta)
    {
        throw new NotImplementedException();
    }

	private void OnTimerTimeout()
    {
        timer.Stop();

        BaseItem inputItem = (BaseItem)ItemConstants.BaseItemScene.Instantiate();

        // Set the position relative to the item node
        inputItem.GlobalPosition = Position - item.GlobalPosition;

        // Set the ItemType (e.g., to Wood)
        inputItem.Create(ItemType.Wood);

        // Add the inputItem to the item node
        item.AddChild(inputItem);
    }
}
