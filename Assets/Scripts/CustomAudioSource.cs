using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioLowPassFilter))]
public class CustomAudioSource : MonoBehaviour
{
    [SerializeField]
    LayerMask wall;
    [SerializeField]
    AudioGroup[] groups;

    AudioSource source;
    AudioLowPassFilter lowPassFilter;

    static Transform listener = null;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        lowPassFilter = GetComponent<AudioLowPassFilter>();
        if (listener == null)
            listener = FindObjectOfType<AudioListener>().transform;
        
    }

    private void Update()
    {
        if (!source.isPlaying) return;


        if (IsWallBetweenListener())
        {
            lowPassFilter.enabled = true;
            source.volume = 0.7f;
        }
        else
        {
            lowPassFilter.enabled = false;
            source.volume = 1f;
        }
    }

    public void PlayRandomFromGroup(string groupName, bool effects = false)
    {
        var group = GetGroupByName(groupName);
        var clip = group.clips[Random.Range(0, group.clips.Length)];
        var volumescale = 1f;
        // Do occlusion and reverb if effects==true
        if (effects)
        {
            if(IsWallBetweenListener())
            {
                Debug.Log("Wall between cat and player. Filter applied.");
                lowPassFilter.enabled = true;
                volumescale = 0.7f;
            }
            else
            {
                lowPassFilter.enabled = false;
            }
        }
        else
        {
            lowPassFilter.enabled = false;
        }
        source.PlayOneShot(clip,volumescale);
    }

    private AudioGroup GetGroupByName(string groupName)
    {
        return groups.Where(g => g.name.Equals(groupName)).First<AudioGroup>();
    }

    bool IsWallBetweenListener()
    {
        Vector3 dir = listener.position - transform.position;
        return Physics.Raycast(transform.position, dir, dir.magnitude, wall);
    }
}
