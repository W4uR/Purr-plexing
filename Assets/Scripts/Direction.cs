using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    NORTH,
    EAST,
    SOUTH,
    WEST
}
public static class DirectionExtensions
{
    public static Vector2Int ToVector2Int(this Direction direction)
    {
        switch (direction)
        {
            case Direction.NORTH:
                return new Vector2Int(0, 1);
            case Direction.EAST:
                return new Vector2Int(1, 0);
            case Direction.SOUTH:
                return new Vector2Int(0, -1);
            case Direction.WEST:
                return new Vector2Int(-1, 0);
            default:
                throw new System.ArgumentException("Invalid direction");
        }
    }
}