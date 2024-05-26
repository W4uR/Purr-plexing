using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        LevelManager.OnLevelLoaded += HandleLevelLoaded;
    }

    private void HandleLevelLoaded(int index)
    {
        text.text = index.ToString();
    }

    private void OnDisable()
    {
        LevelManager.OnLevelLoaded -= HandleLevelLoaded;
    }
}
