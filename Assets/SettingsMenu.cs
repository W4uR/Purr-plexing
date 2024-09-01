using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    AudioMixer mixer;

    [Header("Sliders")]
    [SerializeField]
    private Slider masterSlider;
    [SerializeField]
    private Slider voiceSlider;
    [SerializeField]
    private Slider breezeSlider;
    [SerializeField]
    private Slider catSlider;
    [SerializeField]
    private Slider engineSlider;
    [SerializeField]
    private Slider stepSlider;

    [SerializeField]
    private Button backButton;


    private void Awake()
    {
        if (SceneManager.GetActiveScene().name.Equals("Game")) return;

        string[] args = Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-defvol")
            {
                PlayerPrefs.DeleteKey(Constants.MASTER_VOL);
                PlayerPrefs.DeleteKey(Constants.NARRATOR_VOL);
                PlayerPrefs.DeleteKey(Constants.BREEZE_VOL);
                PlayerPrefs.DeleteKey(Constants.MAPELEMENTS_VOL);
                PlayerPrefs.DeleteKey(Constants.ENGINE_VOL);
                PlayerPrefs.DeleteKey(Constants.PALYER_VOL);
                break;
            }
        }
    }


    private void Start()
    {
        AddListeners();
        LoadPlayerPrefs();
        gameObject.SetActive(false);
    }
    
    private void AddListeners()
    {
        masterSlider.onValueChanged.AddListener(delegate { OnValueChange(masterSlider, Constants.MASTER_VOL); });
        voiceSlider.onValueChanged.AddListener(delegate { OnValueChange(voiceSlider, Constants.NARRATOR_VOL); });
        breezeSlider.onValueChanged.AddListener(delegate { OnValueChange(breezeSlider, Constants.BREEZE_VOL); });
        catSlider.onValueChanged.AddListener(delegate { OnValueChange(catSlider, Constants.MAPELEMENTS_VOL); });
        engineSlider.onValueChanged.AddListener(delegate { OnValueChange(engineSlider, Constants.ENGINE_VOL); });
        stepSlider.onValueChanged.AddListener(delegate { OnValueChange(stepSlider, Constants.PALYER_VOL); });

        backButton.onClick.AddListener( delegate {GameObject.FindGameObjectWithTag("Root Menu").SetActive(true); gameObject.SetActive(false); });
    }

    private void OnValueChange(Slider slider, string mixerParam)
    {
        mixer.SetFloat(mixerParam,Mathf.Lerp(-80,0,slider.value/slider.maxValue));
        PlayerPrefs.SetFloat(mixerParam, slider.value);
    }

    void LoadPlayerPrefs()
    {
        masterSlider.value = PlayerPrefs.GetFloat(Constants.MASTER_VOL, 16);
        voiceSlider.value = PlayerPrefs.GetFloat(Constants.NARRATOR_VOL, 16);
        breezeSlider.value = PlayerPrefs.GetFloat(Constants.BREEZE_VOL, 16);
        catSlider.value = PlayerPrefs.GetFloat(Constants.MAPELEMENTS_VOL, 16);
        engineSlider.value = PlayerPrefs.GetFloat(Constants.ENGINE_VOL, 16);
        stepSlider.value = PlayerPrefs.GetFloat(Constants.PALYER_VOL, 16);

        mixer.SetFloat(Constants.MASTER_VOL, Mathf.Lerp(-80, 0, masterSlider.value / masterSlider.maxValue));
        mixer.SetFloat(Constants.NARRATOR_VOL, Mathf.Lerp(-80, 0, voiceSlider.value / voiceSlider.maxValue));
        mixer.SetFloat(Constants.BREEZE_VOL, Mathf.Lerp(-80, 0, breezeSlider.value / breezeSlider.maxValue));
        mixer.SetFloat(Constants.MAPELEMENTS_VOL, Mathf.Lerp(-80, 0, catSlider.value / catSlider.maxValue));
        mixer.SetFloat(Constants.ENGINE_VOL, Mathf.Lerp(-80, 0, engineSlider.value / engineSlider.maxValue));
        mixer.SetFloat(Constants.PALYER_VOL, Mathf.Lerp(-80, 0, stepSlider.value / stepSlider.maxValue));
    }

}
