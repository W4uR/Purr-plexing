using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

[RequireComponent(typeof(LocalizeStringEvent))]
[RequireComponent(typeof(LocalizeAudioClipEvent))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Button))]
public class ButtonNarrator : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnSelect(BaseEventData eventData)
    {
       audioSource.Play();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        audioSource.Stop();
    }
}
