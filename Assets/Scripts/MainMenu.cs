using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class MainMenu : MonoBehaviour
{
    public void OnPlayTutorialClicked()
    {
        GameManager.Instance.StartGame(0);
    }
    public void OnQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }




}
