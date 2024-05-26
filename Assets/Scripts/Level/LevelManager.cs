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
    public static event Action<int> OnLevelLoaded;

    public static Transform GetLevelParent() => instance.levelParent;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadLevel(GameManager.Instance.LevelToLoad);
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
        Debug.Log($"LevelManager::LoadLevel({levelIndex})");
        if(PlayerPrefs.GetInt(Constants.UNLOCKED_LEVELS,0) < levelIndex)
        {
            PlayerPrefs.SetInt(Constants.UNLOCKED_LEVELS, levelIndex);
        }
        currentLevelIndex = levelIndex;
        CurrentLevel = instance.levels[currentLevelIndex];
        CurrentLevel.Initialize(instance.levelParent);
        OnLevelLoaded.Invoke(levelIndex);
    }
}
