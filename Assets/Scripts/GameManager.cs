using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{

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
