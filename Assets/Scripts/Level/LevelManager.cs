using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    Transform levelParent;
    [SerializeField]
    Level[] levels;

    private static LevelManager instance;
    private static int currentLevelIndex = 0;
    public static Level CurrentLevel { get; private set; }
    public static event Action OnLevelLoaded;

    public static Transform GetLevelParent() => instance.levelParent;

    private void Awake()
    {
        instance = this;
    }

    public static int GetNumberOfCatsOnCurrentLevel()
    {
        return CurrentLevel.CatsOnLevel;
    }

    public static int GetNumberOfLevels()
    {
        return instance.levels.Length;
    }

    public static void LoadLevel(int levelIndex)
    {
        currentLevelIndex = levelIndex;
        CurrentLevel = instance.levels[currentLevelIndex];
        CurrentLevel.Initialize(instance.levelParent);
        OnLevelLoaded.Invoke();
    }
}
