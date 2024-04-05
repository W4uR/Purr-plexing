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
    public static Vector3 ToVector3(this AbsoluteDirection direction)
    {
        switch (direction)
        {
            case AbsoluteDirection.NORTH:
                return Vector3.forward;
            case AbsoluteDirection.EAST:
                return Vector3.right;
            case AbsoluteDirection.SOUTH:
                return Vector3.back;
            case AbsoluteDirection.WEST:
                return Vector3.left;
            default:
                throw new System.ArgumentException("Invalid direction");
        }
    }

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

    public static AbsoluteDirection Opposite(this AbsoluteDirection direction)
    {
        switch (direction)
        {
            case AbsoluteDirection.NORTH:
                return AbsoluteDirection.SOUTH;
            case AbsoluteDirection.EAST:
                return AbsoluteDirection.WEST;
            case AbsoluteDirection.SOUTH:
                return AbsoluteDirection.NORTH;
            case AbsoluteDirection.WEST:
                return AbsoluteDirection.EAST;
            default:
                throw new System.ArgumentException("Invalid direction");
        }
    }
}