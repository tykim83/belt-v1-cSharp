using Godot;

namespace Beltv1C.Buildings.Scripts;

public abstract partial class BaseBuilding : Node2D
{
    protected Sprite2D buildingSprite;
    protected Sprite2D buildingArrow;
    protected Node2D item;

    public abstract void RotateBuilding(bool needsCorner);
    public abstract Vector2I Create(TileMapLayer receivedTilemap, Vector2 mousePosition);
    public abstract void SetNext(Node2D receivedNext);
    public abstract void MoveBoxToNext(float delta);
}
