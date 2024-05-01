using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuInputHandler : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private Button newGameButton;
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private Button levelSelectionButton;


    private void Awake()
    {
        newGameButton.onClick.AddListener(OnNewGameClicked);
        continueButton.onClick.AddListener(OnContinueClicked);
        quitButton.onClick.AddListener(OnQuitClicked);

        HideButtons();
    }

    void HideButtons()
    {
        if(PlayerPrefs.GetInt(Utility.PlayerPrefKey.UNLOCKED_LEVELS.ToString(), 0) == 0)
        {
            continueButton.gameObject.SetActive(false);
            levelSelectionButton.gameObject.SetActive(false);
        }
    }


    public void OnNewGameClicked()
    {
        GameManager.Instance.StartGame(0);
    }

    public void OnContinueClicked()
    {
        GameManager.Instance.StartGame(PlayerPrefs.GetInt(Utility.PlayerPrefKey.UNLOCKED_LEVELS.ToString()));
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
