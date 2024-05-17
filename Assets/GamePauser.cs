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

    private static GamePauser s_instance;
    private void Awake()
    {
        if (s_instance = null)
            s_instance = this;
    }

    public void OnTogglePauseMenu()
    {
        Debug.Log("Pause button pressed.");
        IsPaused = !IsPaused;
        pauseMenu.SetActive(IsPaused);
        settingsMenu.SetActive(false);
    }

    public static void TogglePause()
    {
        s_instance.OnTogglePauseMenu();
    }
}
