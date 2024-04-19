using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageManager : MonoBehaviour
{
    private const string LANGUAGE_KEY = "LanguageKey";

    private bool changeInProgress = false;

    public static LanguageManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey(LANGUAGE_KEY))
        {
            PlayerPrefs.SetInt(LANGUAGE_KEY, LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale));
        }
        StartCoroutine(SetLanguage(PlayerPrefs.GetInt(LANGUAGE_KEY, 0)));
        Debug.Log(System.Globalization.CultureInfo.CurrentUICulture);
        Debug.Log(LocalizationSettings.AvailableLocales.Locales[1]);
    }

    public void OnChangeLanguageClicked()
    {
        if (changeInProgress) return;
        int id = LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
        id = ++id % LocalizationSettings.AvailableLocales.Locales.Count;
        StartCoroutine(SetLanguage(id));
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
