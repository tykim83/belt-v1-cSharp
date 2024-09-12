namespace Beltv1C.Buildings.Enums;

public enum Direction
{
	Right = 0,
	Down = 1,
	Left = 2,
	Up = 3,
	None = 4,
	All = 5
}

public static class DirectionExtensions
{
    public static Direction RotateClockwise(this Direction currentDirection)
    {
        return currentDirection switch
        {
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            Direction.Up => Direction.Right,
            _ => currentDirection,
        };

    }

    public static Direction GetOppositeDirection(this Direction direction)
    {
        return direction switch
        {
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            Direction.Up => Direction.Down,
            Direction.Down => Direction.Up,
            _ => Direction.None,
        };

    }
}