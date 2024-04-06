using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    // At some point this should be  a seperate GameConig file or something like that
    public static bool VisualAids { get; internal set; } = true;

    private int _levelToLoad;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void StartGame(int levelToLoad)
    {
        Debug.Log("Starting game...");
        _levelToLoad = levelToLoad;
        SceneManager.LoadScene("Game");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        if (scene.name.Equals("Game"))
        {
            // Pálya betöltése
            LevelManager.LoadLevel(_levelToLoad);
        }
    }

    public IEnumerator LevelFinished()
    {
        _levelToLoad++;
        yield return new WaitForSeconds(1); // Wait for player to finish moving Or else the end of movement teleport overwrites the teleport to spawnpoint
        if (LevelManager.GetNumberOfLevels() > _levelToLoad)
            LevelManager.LoadLevel(_levelToLoad);
        else
            BackToMenu();
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
