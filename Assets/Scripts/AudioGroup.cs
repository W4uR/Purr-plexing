using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Group", menuName = "Audio/Audio Group")]
public class AudioGroup : ScriptableObject
{
    [SerializeField]
    private AudioClip[] clips;

    public AudioClip GetRandomClip()
    {
        int index = Random.Range(0, clips.Length);
        return clips[index];
    }

}
