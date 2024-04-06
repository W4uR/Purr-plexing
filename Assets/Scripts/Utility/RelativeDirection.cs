using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RelativeDirection
{
    FORWARD,
    RIGHT,
    BACK,
    LEFT
}

public static class RelativeDirectionExtension
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

    public static float ToPanStereo(this RelativeDirection direction)
    {
        switch (direction)
        {
            case RelativeDirection.FORWARD:
                return 0f;
            case RelativeDirection.RIGHT:
                return 1f;
            case RelativeDirection.BACK:
                return 0f;
            case RelativeDirection.LEFT:
                return -1f;
            default:
                throw new System.ArgumentException("Invalid direction");
        }
    }

    public static Vector3 ToVector3(this RelativeDirection direction,Transform reference)
    {
        switch (direction)
        {
            case RelativeDirection.FORWARD:
                return reference.forward;
            case RelativeDirection.RIGHT:
                return reference.right;
            case RelativeDirection.BACK:
                return -reference.forward;
            case RelativeDirection.LEFT:
                return -reference.right;
            default:
                throw new System.ArgumentException("Invalid direction");
        }
    }
}
