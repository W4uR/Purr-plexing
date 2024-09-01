using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Events;

public static class Utility
{

}

public static class Constants
{
    public const string UNLOCKED_LEVELS = "Unlocked_Levels";
    public const string LANGUAGE = "Language";
    public const string MASTER_VOL = "MasterVolume";
    public const string NARRATOR_VOL = "NarratorVolume";
    public const string BREEZE_VOL = "BreezeVolume";
    public const string MAPELEMENTS_VOL = "MapElementsVolume";
    public const string ENGINE_VOL = "EngineVolume";
    public const string PALYER_VOL = "PlayerVolume";

}

public static class Extensions
{
    public static Vector2Int ToVector2Int(this Vector3 vector3)
    {
        return new Vector2Int(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.z));
    }

    public static Vector3 RoundXZ(this Vector3 vector3)
    {
        return new Vector3(Mathf.RoundToInt(vector3.x),vector3.y, Mathf.RoundToInt(vector3.z));
    }
}