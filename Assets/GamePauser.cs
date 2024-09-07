using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class GamePauser : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject settingsMenu;
    [SerializeField]
    private InputSystemUIInputModule UIInputModule;

    public List<MonoBehaviour> componentsToDisableOnPause;

    public static GamePauser Instance;

    private void Awake()
    {
        Instance = this;
        componentsToDisableOnPause = new();
    }

    private bool isPaused;
    public bool IsPaused
    {
        get { return isPaused; }
        set
        {
            isPaused = value;
            if (isPaused)
            {
                Time.timeScale = 0f;

            }
            else
            {
                Time.timeScale = 1f;

            }
            UIInputModule.enabled = isPaused;
            foreach (var component in componentsToDisableOnPause)
            {
                component.enabled = !IsPaused;
            }

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
    }

}
