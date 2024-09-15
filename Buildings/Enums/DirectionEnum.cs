using System;

namespace Beltv1C.Buildings.Enums;

[Flags]
public enum Direction
{
    None = 0,
	Right = 1 << 0,
	Down = 1 << 1,
	Left = 1 << 2,
	Up = 1 << 3,
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