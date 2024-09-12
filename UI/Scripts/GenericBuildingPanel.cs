using Beltv1C.Buildings.Enums;
using Beltv1C.Buildings.Scripts;
using Godot;

namespace Beltv1C.UI.Scripts;	

public partial class GenericBuildingPanel : Panel
{
	[Export]
	private BuildingType buildingType;

    public override void _GuiInput(InputEvent @event)
    {
        if (Input.IsActionJustPressed("click"))
        {
			GD.Print("Click detected.");
			var buildingManager = BuildingManager.Instance;

			if (buildingManager is null)
			{
				GD.PrintErr("BuildingManager instance not found.");
				return;
			}

			// Emit the centralized signal
			buildingManager.EmitSignal(nameof(BuildingManager.BuildingTypeSignal), (int)buildingType);
        }
    }
}
