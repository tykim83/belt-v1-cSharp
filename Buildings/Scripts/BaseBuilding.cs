using Godot;
using Beltv1C.Items.Scripts;
using Beltv1C.Buildings.Enums;
using Beltv1C.Buildings.Constants;

namespace Beltv1C.Buildings.Scripts;

public abstract partial class BaseBuilding : Node2D
{
    protected Sprite2D buildingSprite;
    protected Sprite2D buildingArrow;
    protected Node2D item;

    protected BuildingType buildingType;
    protected Direction directionFrom;
    protected Direction directionTo;

    protected BaseBuilding next = null;
    protected BaseItem inputItem = null;
    protected bool hasArrived = false;
    protected BaseItem outputItem = null;

    public BuildingType BuildingType => buildingType;
    public Direction DirectionFrom => directionFrom;
    public Direction DirectionTo => directionTo;


    public virtual void RotateBuilding(bool needsCorner) 
    {
        buildingSprite.RotationDegrees += 90;
        directionTo = directionTo.RotateClockwise();
    }

    public virtual void Create() 
    {
        Color currentColor = buildingSprite.Modulate;
        currentColor.A = 1f; 
        buildingSprite.Modulate = currentColor;
    }

    public virtual void SetNext(BaseBuilding receivedNext) 
    {
        next = receivedNext;
        buildingArrow.Visible = true;
        buildingArrow.LookAt(next.GlobalPosition);
    }

    public virtual void StartSendingItem(BaseItem itemToSend) 
    {
        inputItem = itemToSend;
    }

    public virtual bool IsReadyToReceive() 
    {
        return inputItem == null;
    }

    public virtual void ItemReceived() 
    {
        hasArrived = true;
    }

    public override void _Process(double delta)
    {   
        if (inputItem != null && hasArrived && outputItem == null && next != null && next.IsReadyToReceive())
        {
            outputItem = inputItem;
            inputItem = null;
            hasArrived = false;
            next.StartSendingItem(outputItem);
            MoveBoxToNext(delta);
        }
        else if (outputItem != null)
        {
            MoveBoxToNext(delta);
        }
    }

    public virtual void MoveBoxToNext(double delta)
    {
        // Move the output item toward the next position
        outputItem.GlobalPosition = outputItem.GlobalPosition.MoveToward(next.Position, (float)(BuildingConstants.MOVEMENT_SPEED * delta));
        
        // Check if the output item is close enough to the next position
        if (outputItem.GlobalPosition.DistanceTo(next.Position) < BuildingConstants.BUFFER_ZONE)
        {
            next.ItemReceived();
            outputItem = null;
        }
    }
}
