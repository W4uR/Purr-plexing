using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{

    #region Pause
    private static bool isPaused;
    public static bool IsPaused {
        get { return isPaused; }
        set
        {
            isPaused = value;
            if(PausedChanged != null)
                PausedChanged.Invoke(value);
        }
    }

    public static event Action<bool> PausedChanged;

    public void OnTogglePauseMenu()
    {
        IsPaused = !IsPaused;
    }
    #endregion

    private int levelToLoad;
    public int LevelToLoad => levelToLoad;

    public void StartGame(int levelToLoad)
    {
        Debug.Log("Starting game...");
        this.levelToLoad = levelToLoad;
        SceneManager.LoadScene("Game");
    }

    public IEnumerator LevelFinished()
    {
        levelToLoad++;
        yield return new WaitForSeconds(1); // Wait for player to finish moving Or else the end of movement teleport overwrites the teleport to spawnpoint
        if (LevelManager.GetNumberOfLevels() > levelToLoad)
            LevelManager.LoadLevel(levelToLoad);
        else
            SceneManager.LoadScene("Menu");
    }
}
