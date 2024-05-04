using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
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
    private Slider birdSlider;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        AddListeners();
        LoadPlayerPrefs();
    }

    private void AddListeners()
    {
        masterSlider.onValueChanged.AddListener(delegate { OnValueChange(masterSlider, Constants.MASTER_VOL); });
        voiceSlider.onValueChanged.AddListener(delegate { OnValueChange(voiceSlider, Constants.VOICE_VOL); });
        breezeSlider.onValueChanged.AddListener(delegate { OnValueChange(breezeSlider, Constants.BREEZE_VOL); });
        catSlider.onValueChanged.AddListener(delegate { OnValueChange(catSlider, Constants.CAT_VOL); });
        birdSlider.onValueChanged.AddListener(delegate { OnValueChange(birdSlider, Constants.BIRD_VOL); });
    }

    private void OnValueChange(Slider slider, string mixerParam)
    {
        if(slider.value == 0)
        {
            mixer.SetFloat(mixerParam, -80f);
        }
        else
        {
            mixer.SetFloat(mixerParam, Mathf.Lerp(-20, 5f, slider.value / slider.maxValue));
        }
        PlayerPrefs.SetFloat(mixerParam, slider.value);
    }

    void LoadPlayerPrefs()
    {
        masterSlider.value = PlayerPrefs.GetFloat(Constants.MASTER_VOL, 8);
        voiceSlider.value = PlayerPrefs.GetFloat(Constants.VOICE_VOL, 8);
        breezeSlider.value = PlayerPrefs.GetFloat(Constants.BREEZE_VOL, 8);
        catSlider.value = PlayerPrefs.GetFloat(Constants.CAT_VOL, 8);
        birdSlider.value = PlayerPrefs.GetFloat(Constants.BIRD_VOL, 8);
    }


}
