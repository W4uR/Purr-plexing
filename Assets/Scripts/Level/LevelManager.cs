using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    Transform levelParent;
    [SerializeField]
    Level[] levels;

    public static LevelManager Instance;
    private static int currentLevelIndex = 0;
    public static Level CurrentLevel { get; private set; }
    public static event Action<int> OnLevelLoaded;

    public static Transform GetLevelParent() => Instance.levelParent;

    private void Awake()
    {
        Instance = this;
    }

    public static int GetNumberOfCatsOnCurrentLevel()
    {
        return CurrentLevel.CatsOnLevel;
    }

    public static int GetNumberOfLevels()
    {
        return Instance.levels.Length;
    }

    public static void LoadLevel(int levelIndex)
    {
        Debug.Log($"LevelManager::LoadLevel({levelIndex})");
        if(levelIndex > GetNumberOfLevels())
        {
            Debug.LogError($"Tried to load level '{levelIndex}' with '{GetNumberOfLevels() - 1}' being the maximum. Returning...");
            return;
        }
        if(PlayerPrefs.GetInt(Constants.UNLOCKED_LEVELS,0) < levelIndex)
        {
            PlayerPrefs.SetInt(Constants.UNLOCKED_LEVELS, levelIndex);
        }
        currentLevelIndex = levelIndex;
        CurrentLevel = Instance.levels[currentLevelIndex];
        CurrentLevel.Initialize(Instance.levelParent);
        OnLevelLoaded.Invoke(levelIndex);
    }

    public static void LoadNextLevel()
    {
        LoadLevel(currentLevelIndex + 1);
    }
}
