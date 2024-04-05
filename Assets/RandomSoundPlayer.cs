using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]

public class RandomSoundPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioGroup[] audioGroups;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomFromGroup(string name)
    {
        AudioClip[] clips = audioGroups.FirstOrDefault(x => x.name.Equals(name)).clips;
        int randomIndex = Random.Range(0, clips.Length);
        audioSource.PlayOneShot(clips[randomIndex]);
    }

    public void PlayRandomFromGroup(AudioGroup group)
    {
        AudioClip[] clips = group.clips;
        int randomIndex = Random.Range(0, clips.Length);
        audioSource.PlayOneShot(clips[randomIndex]);
    }
}
