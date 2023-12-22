using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbsoluteDirection
{
    NORTH,
    EAST,
    SOUTH,
    WEST
}
public static class AbsoluteDirectionExtensions
{
    public static Vector2Int ToVector2Int(this AbsoluteDirection direction)
    {
        switch (direction)
        {
            case AbsoluteDirection.NORTH:
                return new Vector2Int(0, 1);
            case AbsoluteDirection.EAST:
                return new Vector2Int(1, 0);
            case AbsoluteDirection.SOUTH:
                return new Vector2Int(0, -1);
            case AbsoluteDirection.WEST:
                return new Vector2Int(-1, 0);
            default:
                throw new System.ArgumentException("Invalid direction");
        }
    }
}