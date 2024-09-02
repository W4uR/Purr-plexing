using System;
using System.Collections;
using System.Threading.Tasks;
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

    static Awaitable narration;

    public static async Awaitable PlayAudioClip(string key)
    {
        
        var clipTask = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<AudioClip>(TABLE_NAME, key);
        var textTask = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(TABLE_NAME, key);

        var clip = clipTask.WaitForCompletion();

        if (clipTask.Result == null)
        {
            Debug.LogError("No key in Asset table");
            return;
        }

        var subtitle =  textTask.WaitForCompletion();
        if (textTask.Result == null)
        {
            Debug.LogError("No key in String table");
            return;
        }

        s_instance.speaker.clip = clip;
        s_instance.speaker.Play();

        s_instance.subtitleArea.text = subtitle;

        await Awaitable.WaitForSecondsAsync(clip.length);
    }
    public static void Clear()
    {
        s_instance.speaker.clip = null;
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
