using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauser : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject settingsMenu;
    [SerializeField]
    List<MonoBehaviour> componentsToDisable;

    private static bool isPaused;
    public static bool IsPaused
    {
        get { return isPaused; }
        set
        {
            isPaused = value;
            if (PausedChanged != null)
                PausedChanged.Invoke(value);
        }
    }

    public static event Action<bool> PausedChanged;

    public void OnTogglePauseMenu()
    {
        Debug.Log("Pause button pressed.");
        IsPaused = !IsPaused;
        pauseMenu.SetActive(IsPaused);
        settingsMenu.SetActive(false);
        foreach (var component in componentsToDisable)
        {
            component.enabled = !IsPaused;
        }
    }

}
