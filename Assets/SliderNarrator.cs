using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SliderNarrator : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [Range(0.5f,1.5f)]
    [SerializeField]
    private float tasteDuration = 1f;

    private Slider mySlider;

    private AudioSource labelAudioSource;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        labelAudioSource = GetComponentsInChildren<AudioSource>().Last();
        mySlider = GetComponent<Slider>();

        mySlider.onValueChanged.AddListener(delegate {
            audioSource.Stop();
            StopAllCoroutines(); 
            StartCoroutine(PlayTaste()); });
    }

    public void OnSelect(BaseEventData eventData)
    {
        labelAudioSource.Play();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        labelAudioSource.Stop();
        audioSource.Stop();
        StopAllCoroutines();
    }

    IEnumerator PlayTaste()
    {
        audioSource.Play();
        yield return new WaitForSeconds(tasteDuration);
        audioSource.Stop();
    }
}
