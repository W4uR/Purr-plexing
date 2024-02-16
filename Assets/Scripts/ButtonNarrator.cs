using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(LocalizeAudioClipEvent))]
[RequireComponent(typeof(AudioSource))]
public class ButtonNarrator : MonoBehaviour, ISelectHandler
{
    private AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    public void OnSelect(BaseEventData eventData)
    {
        source.Play();
    }
}
