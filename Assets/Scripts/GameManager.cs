using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{

    private int levelToLoad;
    public int LevelToLoad => levelToLoad;

    private void Start()
    {

        string[] args = Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-fresh")
            {
                PlayerPrefs.DeleteAll();
                break;
            }
        }
    }

    public void StartGame(int levelToLoad)
    {
        Debug.Log("Starting game...");
        this.levelToLoad = levelToLoad;
        SceneManager.LoadScene("Game");
    }



    public void LevelFinished()
    {
        StartCoroutine(LevelFinishedCorutine());
    }

    public IEnumerator LevelFinishedCorutine()
    {
        Debug.Log("GameManager::LevelFinished");

        if(levelToLoad == 0)//We are in the tutorial
        {
            yield return new WaitForSeconds(2f);
            yield return StartCoroutine(Tutorial.LevelFinished());
        }


        levelToLoad++;
        yield return new WaitForSeconds(1); // Wait for player to finish moving Or else the end of movement teleport overwrites the teleport to spawnpoint
        if (LevelManager.GetNumberOfLevels() > levelToLoad)
            LevelManager.LoadLevel(levelToLoad);
        else
            SceneManager.LoadScene("Menu");
    }
}
