using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreezeDisplay : MonoBehaviour
{
    private static BreezeDisplay s_instance;

    [SerializeField]
    private Image leftBreezeImage;
    [SerializeField]
    private Image rightBreezeImage;

    [SerializeField]
    private Sprite[] breezeIcons;

    private void Awake()
    {
        s_instance = this;
    }

    internal static void DisplayLeft(float v)
    {
        if (v == 0f)
            s_instance.leftBreezeImage.enabled = false;
        else
        {
            s_instance.leftBreezeImage.enabled = true;
            s_instance.leftBreezeImage.sprite = s_instance.breezeIcons[Mathf.CeilToInt(v * s_instance.breezeIcons.Length)-1];
        }
    }

    internal static void DisplayRight(float v)
    {
        if (v == 0f)
            s_instance.rightBreezeImage.enabled = false;
        else
        {
            s_instance.rightBreezeImage.enabled = true;
            s_instance.rightBreezeImage.sprite = s_instance.breezeIcons[Mathf.CeilToInt(v * s_instance.breezeIcons.Length) - 1];
        }

    }
}
