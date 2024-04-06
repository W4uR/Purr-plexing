using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class UIManager : MonoBehaviour
{
    private const string LANGUAGE_KEY = "LanguageKey";

    private bool changeInProgress = false;

    private void Start()
    {
        StartCoroutine(SetLanguage(PlayerPrefs.GetInt(LANGUAGE_KEY, 0)));
    }

    public void OnPlayTutorialClicked()
    {
        GameManager.Instance.StartGame(0);
    }

    public void OnChangeLanguageClicked()
    {
        if (changeInProgress) return;
        int id = LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
        id = ++id % LocalizationSettings.AvailableLocales.Locales.Count;
        StartCoroutine(SetLanguage(id));
    }

    public void OnQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }

    IEnumerator SetLanguage(int id)
    {
        changeInProgress = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[id];
        PlayerPrefs.SetInt(LANGUAGE_KEY, id);
        changeInProgress = false;
    }
}
