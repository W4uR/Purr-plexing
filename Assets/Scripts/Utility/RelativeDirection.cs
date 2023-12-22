using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RelativeDirection
{
    LEFT,
    RIGHT
}

public static class RelativeDirectionEXtension
{
    public static float toRotationAngle(this RelativeDirection direction)
    {
        switch (direction)
        {
            case RelativeDirection.LEFT:
                return -90f;
            case RelativeDirection.RIGHT:
                return 90f;
            default:
                throw new System.ArgumentException("Invalid direction");
        }
    }

    public static float toStereo(this RelativeDirection direction)
    {
        switch (direction)
        {
            case RelativeDirection.LEFT:
                return -1f;
            case RelativeDirection.RIGHT:
                return 1f;
            default:
                throw new System.ArgumentException("Invalid direction");
        }
    }
}
