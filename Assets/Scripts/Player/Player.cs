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

    public static void TeleportTo(Vector3 position, bool resetRotation = true)
    {
        instance.transform.position = new Vector3(position.x, instance.transform.position.y, position.z);
        instance.transform.rotation = Quaternion.Euler(Vector3.forward);
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
