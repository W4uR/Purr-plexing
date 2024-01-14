using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Floor Material", menuName = "Floor Material")]
public class FloorMaterial : ScriptableObject
{
    public AudioClip[] stepSounds;

    public AudioClip GetRandomSound()
    {
        return stepSounds[Random.Range(0, stepSounds.Length)];
    }
}
