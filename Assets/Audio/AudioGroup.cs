using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Group", menuName = "Audio/Audio Group")]
public class AudioGroup : ScriptableObject
{
    public AudioClip[] clips;
}
