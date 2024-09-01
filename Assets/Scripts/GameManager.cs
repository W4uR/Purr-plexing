using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    private void Start()
    {

        string[] args = Environment.GetCommandLineArgs();
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-fresh")
            {
                PlayerPrefs.DeleteAll();
                break;
            }
        }
    }

    public async void StartGame(int levelToLoad)
    {
        Debug.Log("Starting game...");
        await SceneManager.LoadSceneAsync("Game");
        LevelManager.LoadLevel(levelToLoad);

    }

    internal void LevelFinished()
    {
        // Pause game
        // Show level finished canvas.
    }
}
