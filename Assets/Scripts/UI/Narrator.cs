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
    [SerializeField]


    private static Narrator s_instance;


    const string TABLE_NAME = "LocalizationTable";

    void Awake()
    { 
        s_instance = this;
    }

    public static IEnumerator PlayAudioClip(string key)
    {
        AudioClip clip = LocalizationSettings.AssetDatabase.GetLocalizedAsset<AudioClip>(TABLE_NAME, key);
        if(clip == null)
        {
            Debug.LogError("No key in table");
            yield break;
        }
        s_instance.speaker.PlayOneShot(clip);
        s_instance.subtitleArea.text = LocalizationSettings.StringDatabase.GetLocalizedString(TABLE_NAME, key);
        yield return new WaitForSeconds(clip.length+1.6f);
    }

}
