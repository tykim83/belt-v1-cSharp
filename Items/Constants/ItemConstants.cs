using Beltv1C.Items.Enums;
using Godot;
using System.Collections.Generic;

namespace Beltv1C.Items.Constants;

public static class ItemConstants
{
    public static readonly PackedScene BaseItemScene = (PackedScene)ResourceLoader.Load("res://Items/Scenes/base-item.tscn");

    private static readonly Dictionary<ItemType, Texture2D> ItemSprites = new()
    {
        { ItemType.None, null },
        { ItemType.Wood, (Texture2D)ResourceLoader.Load("res://Art/Items/wood_log.png") },
        { ItemType.Planks, (Texture2D)ResourceLoader.Load("res://Art/Items/wood_plank.png") },
        { ItemType.Arrow, (Texture2D)ResourceLoader.Load("res://Art/Items/wood_arrow.png") },
    };

    public static Texture2D GetItemSprite(ItemType itemType)
    {
        if (!ItemSprites.ContainsKey(itemType))
        {
            GD.PrintErr($"Sprite not found for ItemType: {itemType}");
            return null;
        }

        return ItemSprites[itemType];
    }
}