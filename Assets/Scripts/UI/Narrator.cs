using System;
using System.Collections;
using System.Threading.Tasks;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(TMP_Text))]
public class Narrator : MonoBehaviour
{
    private TMP_Text subtitleArea;
    private AudioSource source;

    [SerializeField]
    StringTableCollection stringTableCollection;
    [SerializeField]
    AssetTableCollection audioClipTableCollection;

    private static Narrator instance;

    void Start()
    {
        subtitleArea = GetComponent<TMP_Text>();
        source = GetComponent<AudioSource>();
        instance = this;
    }

    public static IEnumerator PlayAudioClip(string key)
    {
        var gettingResource = ((AssetTable)instance.audioClipTableCollection.GetTable(LocalizationSettings.SelectedLocale.Identifier)).GetAssetAsync<AudioClip>(key);
        yield return gettingResource.Task;

        //Display subtitle if Visual Aids are on.
        if (GameManager.VisualAids)
        {
            instance.subtitleArea.text = ((StringTable)instance.stringTableCollection.GetTable(LocalizationSettings.SelectedLocale.Identifier)).GetEntry(key).LocalizedValue;
        }

        // After successful resource fetch, play audio.
        if (gettingResource.Status == AsyncOperationStatus.Succeeded && gettingResource.Result != null)
        {
            instance.source.PlayOneShot(gettingResource.Result);
            yield return new WaitForSeconds(gettingResource.Result.length);
        }
        else
        {
            yield return new WaitForSeconds(2);
        }
    }
}
