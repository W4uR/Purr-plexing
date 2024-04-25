using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class DisplayableAudio : MonoBehaviour
{
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private AudioSource audioSource;

    public Sprite Icon => icon;
    public float Clarity =>audioSource.volume;
    public bool Active => !audioSource.isVirtual && audioSource.isPlaying;


    private void OnDestroy()
    {
        Compass.DetachAudio(this);
    }

    private void Start()
    {
        Compass.AttachAudio(this);
    }
}
