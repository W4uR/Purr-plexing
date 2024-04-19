using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

[RequireComponent(typeof(LocalizeStringEvent))]
[RequireComponent(typeof(LocalizeAudioClipEvent))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Button))]
public class ButtonNarrator : MonoBehaviour, ISelectHandler
{
    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        _audioSource.Play();
    }
}
