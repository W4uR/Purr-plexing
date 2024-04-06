using System;
using System.Collections;
using System.Threading.Tasks;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Localization;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(TMP_Text))]
public class Narrator : MonoBehaviour
{
    private TMP_Text subtitleArea;
    private AudioSource source;
    private static Narrator instance;


    const string TABLE_NAME = "Narration";

    void Start()
    {
        subtitleArea = GetComponent<TMP_Text>();
        source = GetComponent<AudioSource>();
        instance = this;
    //    source.PlayOneShot(LocalizationSettings.AssetDatabase.GetLocalizedAsset<AudioClip>(TABLE_NAME, "tutorial.intro.1"));
    }

    public static IEnumerator PlayAudioClip(string key)
    {
        AudioClip clip = LocalizationSettings.AssetDatabase.GetLocalizedAsset<AudioClip>(TABLE_NAME, key);
        instance.source.PlayOneShot(clip);
        instance.subtitleArea.text = LocalizationSettings.StringDatabase.GetLocalizedString(TABLE_NAME, key);
        yield return new WaitForSeconds(clip.length+1.6f);
    }

}
