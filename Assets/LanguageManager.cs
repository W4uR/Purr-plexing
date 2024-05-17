using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageManager : MonoBehaviour
{

    private bool changingLanguage = false;

    private int selectedLocaleIndex = -1;

    void Awake()
    {
        DetermineStartingLocale();
    }

    void DetermineStartingLocale()
    {
        // Command line
        string[] args = Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-lang")
            {
                if (i + 1 < args.Length)
                {
                    string locale = args[i + 1];
                    var selecetedLocale = LocalizationSettings.AvailableLocales.Locales.First(l => l.Identifier.Code.Equals(locale));

                    if (selecetedLocale != null)
                    {
                        ChangeLocale(LocalizationSettings.AvailableLocales.Locales.IndexOf(selecetedLocale));
                    }
                }
                break;
            }
        }
        //Playerprefs
        if (selectedLocaleIndex == -1)
        {
            if (PlayerPrefs.HasKey(Constants.LANGUAGE))
            {
                ChangeLocale(PlayerPrefs.GetInt(Constants.LANGUAGE));
            }
        }
        //System language
        if (selectedLocaleIndex == -1)
        {
            string languageCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var selecetedLocale = LocalizationSettings.AvailableLocales.Locales.First(l => l.Identifier.Code.Equals(languageCode));
            if (selecetedLocale != null)
            {
                ChangeLocale(LocalizationSettings.AvailableLocales.Locales.IndexOf(selecetedLocale));
            }
        }
        //Default
        if (selectedLocaleIndex == -1)
        {
            ChangeLocale(0);
        }
    }


    void ChangeLocale(int localeId)
    {
        if (changingLanguage) return;
        selectedLocaleIndex = localeId;
        PlayerPrefs.SetInt(Constants.LANGUAGE, localeId);
        StartCoroutine(SetLocale(localeId));
    }

    IEnumerator SetLocale(int localeId)
    {
        changingLanguage = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeId];
        changingLanguage = false;
    }
}
