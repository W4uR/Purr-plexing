using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    LevelManager levelManager = null;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void StartGame()
    {
        Debug.Log("Starting game...");
        SceneManager.LoadScene("Game");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        if (scene.name.Equals("Game"))
        {
            // J�t�k v�ltoz�k inicializ�l�sa
            levelManager = FindObjectOfType<LevelManager>();
            // P�lya bet�lt�se
            levelManager.LoadLevel(0);
        }
    }

    internal void LevelFinished()
    {
        Invoke(nameof(BackToMenu), 3f);
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
