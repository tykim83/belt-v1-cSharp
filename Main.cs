using Beltv1C.Buildings.Scripts;
using Beltv1C.Items.Enums;
using Beltv1C.Items.Scripts;
using Godot;

namespace Beltv1C;

public partial class Main : Node2D
{

	public override void _Ready()
	{
		// // Create a new instance of the BaseItem
		// var itemScene = GD.Load<PackedScene>("res://Items/Scenes/base-item.tscn");
		// BaseItem newItem = (BaseItem)itemScene.Instantiate();

		// // Add the new item to the scene tree, under the Main node
		// AddChild(newItem);

		// // Create a new item of type Wood
		// newItem.Create(ItemType.Wood);


		// Load the building scene
        var buildingScene = GD.Load<PackedScene>("res://Buildings/Scenes/generator.tscn");
        
        // Instantiate the building
        var generator = (Generator)buildingScene.Instantiate();
        
        // Set the building's position for testing
        generator.Position = new Vector2(200, 200);
        
        // Add the building to the Main scene (this)
        AddChild(generator);

        // Optional: Print confirmation to the console
        GD.Print("Building added for testing.");
	}
}
