using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;


public class Narrator : MonoBehaviour
{
    [SerializeField]
    private TMP_Text subtitleArea;
    [SerializeField]
    private AudioSource speaker;

    private static Narrator s_instance;


    const string TABLE_NAME = "SpeechTable";

    void Awake()
    { 
        s_instance = this;
    }

    private void OnEnable()
    {
        GamePauser.PausedChanged += OnPauseChanged;
    }

    private void OnDisable()
    {
        GamePauser.PausedChanged -= OnPauseChanged;

    }

    public static IEnumerator PlayAudioClip(string key)
    {

        AudioClip clip = LocalizationSettings.AssetDatabase.GetLocalizedAsset<AudioClip>(TABLE_NAME, key);
        if(clip == null)
        {
            Debug.LogError("No key in table");
            yield break;
        }
        while (GamePauser.IsPaused)
        {
            Debug.Log("The Game is Paused");
            yield return null;
        }
        s_instance.speaker.clip = clip;
        var timeleft = clip.length;


        s_instance.speaker.Play();
        s_instance.subtitleArea.text = LocalizationSettings.StringDatabase.GetLocalizedString(TABLE_NAME, key);
        while(timeleft >= 0f)
        {
            timeleft -= Time.deltaTime;
            while (GamePauser.IsPaused)
            {
                Debug.Log("The Game is Paused");
                yield return null;
            }
            yield return new WaitForEndOfFrame();
        }
        s_instance.speaker.clip = null;
    }
    public static void Clear()
    {
        s_instance.subtitleArea.text = "";
    }

    void OnPauseChanged(bool isPaused)
    {
        if(isPaused)
            speaker.Pause();
        else
            speaker.Play();
    }

}
