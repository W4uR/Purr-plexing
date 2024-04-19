using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class DisplayableAudio : MonoBehaviour
{
    [SerializeField]
    private Sprite _icon;

    private AudioSource _audioSource;

    public Sprite GetIcon() => _icon;
    public float GetClarity() =>_audioSource.volume;


    private void OnDestroy()
    {
        Compass.DetachAudio(this);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (_audioSource.isVirtual || !_audioSource.isPlaying)
        {
            Compass.DetachAudio(this);
        }
        else
        {
            Compass.AttachAudio(this);
        }
    }
}
