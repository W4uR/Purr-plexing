using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    private void Awake()
    {
        instance = this;
    }

    public static void TeleportTo(Vector3 position)
    {
        instance.transform.position = position;
    }

    public static Vector3 GetPosition()
    {
        return instance.transform.position;
    }

    public static Player GetInstance()
    {
        return instance;
    }
}
