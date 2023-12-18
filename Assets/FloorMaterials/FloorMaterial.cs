using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Floor Material", menuName = "Custom/Floor Material")]
public class FloorMaterial : ScriptableObject
{
    public List<AudioClip> stepSounds;

    public AudioClip getRandomSound()
    {
        return stepSounds[Random.Range(0, stepSounds.Count)];
    }
}
