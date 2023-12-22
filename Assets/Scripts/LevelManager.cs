using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    List<Level> levels;

    [SerializeField]
    Transform levelParent;

    private int currentLevelIndex = 0;
    private static Level currentLevel = null;


    private void Start()
    {
        LoadLevel(currentLevelIndex);
    }

    public static Cell getCell(Vector3 worldPosition)
    {
        return currentLevel.GetCell(worldPosition);
    }

    public void LoadLevel(int levelIndex)
    {
        foreach (Transform cell in levelParent)
        {
            Destroy(cell.gameObject);
        }
        currentLevelIndex = levelIndex;
        currentLevel = levels[currentLevelIndex];
        currentLevel.initialize(levelParent);
    }

}
