using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSoundEffects : Singleton<GlobalSoundEffects>
{
    [SerializeField]
    AudioSource audioSource;

    [Header("Effects")]
    [SerializeField]
    AudioClip catPickupEffect;
    [SerializeField]
    AudioClip catSavedEffect;
    [SerializeField]
    AudioClip allCatsSavedEffect;

    public void PlayCatPickUp()
    {
        audioSource.PlayOneShot(catPickupEffect);
    }
    public void PlayCatSaved()
    {
        audioSource.PlayOneShot(catSavedEffect);
    }
    public void PlayAllCatsSaved()
    {
        audioSource.PlayOneShot(allCatsSavedEffect,.5f);
    }
}
