using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    // At some point this should be  a seperate GameConig file or something like that
    public static bool VisualAids { get; internal set; } = true;

    public int levelToLoad;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
            // Pálya betöltése
            LevelManager.LoadLevel(levelToLoad);
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
