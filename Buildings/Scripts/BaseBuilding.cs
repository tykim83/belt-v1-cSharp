using Godot;
using Beltv1C.Items.Enums;
using Beltv1C.Items.Scripts;
using Beltv1C.Buildings.Enums;

namespace Beltv1C.Buildings.Scripts;

public abstract partial class BaseBuilding : Node2D
{
    protected Sprite2D buildingSprite;
    protected Sprite2D buildingArrow;
    protected Node2D item;

    protected ItemType itemType;
    protected Direction directionFrom;
    protected Direction directionTo;

    protected Node2D next = null;
    protected BaseItem inputItem = null;
    protected BaseItem outputItem = null;

    public virtual void RotateBuilding(bool needsCorner) 
    {
        buildingSprite.RotationDegrees += 90;
        directionTo = directionTo.RotateClockwise();
    }

    public abstract Vector2I Create(TileMapLayer receivedTilemap, Vector2 mousePosition);
    public abstract void SetNext(Node2D receivedNext);
    public abstract void MoveBoxToNext(float delta);
}
