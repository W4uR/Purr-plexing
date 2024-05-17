using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Localization.Settings;
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
    private Slider ambianceSlider;


    private void Awake()
    {
        string[] args = Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-defvol")
            {
                PlayerPrefs.DeleteKey(Constants.MASTER_VOL);
                PlayerPrefs.DeleteKey(Constants.VOICE_VOL);
                PlayerPrefs.DeleteKey(Constants.BREEZE_VOL);
                PlayerPrefs.DeleteKey(Constants.CAT_VOL);
                PlayerPrefs.DeleteKey(Constants.ENGINE_VOL);
                PlayerPrefs.DeleteKey(Constants.STEP_VOL);
                PlayerPrefs.DeleteKey(Constants.AMBIANCE_VOL);
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
        voiceSlider.onValueChanged.AddListener(delegate { OnValueChange(voiceSlider, Constants.VOICE_VOL); });
        breezeSlider.onValueChanged.AddListener(delegate { OnValueChange(breezeSlider, Constants.BREEZE_VOL); });
        catSlider.onValueChanged.AddListener(delegate { OnValueChange(catSlider, Constants.CAT_VOL); });
        engineSlider.onValueChanged.AddListener(delegate { OnValueChange(engineSlider, Constants.ENGINE_VOL); });
        stepSlider.onValueChanged.AddListener(delegate { OnValueChange(stepSlider, Constants.STEP_VOL); });
        ambianceSlider.onValueChanged.AddListener(delegate { OnValueChange(ambianceSlider, Constants.AMBIANCE_VOL); });
    }

    private void OnValueChange(Slider slider, string mixerParam)
    {
        mixer.SetFloat(mixerParam,Mathf.Lerp(-80,0,slider.value/slider.maxValue));
        PlayerPrefs.SetFloat(mixerParam, slider.value);
    }

    void LoadPlayerPrefs()
    {
        masterSlider.value = PlayerPrefs.GetFloat(Constants.MASTER_VOL, 16);
        voiceSlider.value = PlayerPrefs.GetFloat(Constants.VOICE_VOL, 16);
        breezeSlider.value = PlayerPrefs.GetFloat(Constants.BREEZE_VOL, 16);
        catSlider.value = PlayerPrefs.GetFloat(Constants.CAT_VOL, 16);
        engineSlider.value = PlayerPrefs.GetFloat(Constants.ENGINE_VOL, 16);
        stepSlider.value = PlayerPrefs.GetFloat(Constants.STEP_VOL, 16);
        ambianceSlider.value = PlayerPrefs.GetFloat(Constants.AMBIANCE_VOL, 2);

        mixer.SetFloat(Constants.MASTER_VOL, Mathf.Lerp(-80, 0, masterSlider.value / masterSlider.maxValue));
        mixer.SetFloat(Constants.VOICE_VOL, Mathf.Lerp(-80, 0, voiceSlider.value / voiceSlider.maxValue));
        mixer.SetFloat(Constants.BREEZE_VOL, Mathf.Lerp(-80, 0, breezeSlider.value / breezeSlider.maxValue));
        mixer.SetFloat(Constants.CAT_VOL, Mathf.Lerp(-80, 0, catSlider.value / catSlider.maxValue));
        mixer.SetFloat(Constants.ENGINE_VOL, Mathf.Lerp(-80, 0, engineSlider.value / engineSlider.maxValue));
        mixer.SetFloat(Constants.STEP_VOL, Mathf.Lerp(-80, 0, stepSlider.value / stepSlider.maxValue));
        mixer.SetFloat(Constants.AMBIANCE_VOL, Mathf.Lerp(-80, 0, ambianceSlider.value / ambianceSlider.maxValue));
    }

}
