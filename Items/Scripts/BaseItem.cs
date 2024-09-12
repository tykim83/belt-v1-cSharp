using Beltv1C.Items.Constants;
using Beltv1C.Items.Enums;
using Godot;

namespace Beltv1C.Items.Scripts;

public partial class BaseItem : Node2D
{
	private Sprite2D sprite;
	public ItemType ItemType { get; private set; } = ItemType.None;

	public override void _Ready()
	{
		
	}

	public void Create(ItemType itemType)
	{
		sprite = GetNode<Sprite2D>("ItemSprite");
		if (sprite == null)
			GD.PrintErr("Failed to find ItemSprite node.");

		ItemType = itemType;
		sprite.Texture = ItemConstants.GetItemSprite(itemType);
		sprite.Scale = new Vector2(0.75f, 0.75f);

		GD.Print($"Item created at position: {Position}");
	}

	public ItemType GetItemType()
	{
		return ItemType;
	}

	public string GetItemName()
	{
		return ItemType.ToString();
	}
}
