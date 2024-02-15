using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    Level[] levels;

    [SerializeField]
    Transform levelParent;

    private int currentLevelIndex = 0;
    private static Level currentLevel = null;

    public static event Action levelLoaded;

    public static Cell GetCell(Vector3 worldPosition)
    {
        return currentLevel.GetCell(worldPosition);
    }
    public static int GetNumberOfCatsOnCurrentLevel()
    {
        return currentLevel.catsOnLevel;
    }

    public static Vector3 GetSpawnPosition()
    {
        return currentLevel.spawnPoint;
    }

    public void LoadLevel(int levelIndex)
    {

        currentLevelIndex = levelIndex;
        currentLevel = levels[currentLevelIndex];
        currentLevel.Initialize(levelParent);
        levelLoaded.Invoke();
    }
}
