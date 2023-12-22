using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{

}

public static class EXtensions
{
    public static Vector2Int ToVector2Int(this Vector3 vector3)
    {
        return new Vector2Int(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.z));
    }
}
