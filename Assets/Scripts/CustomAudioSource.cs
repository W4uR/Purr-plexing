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

    public void PlayRandomFromGroup(string groupName, bool effects = false)
    {
        var group = GetGroupByName(groupName);
        var clip = group.clips[Random.Range(0, group.clips.Length)];
        var volumescale = 1f;
        // Do occlusion and reverb if effects==true
        if (effects)
        {
            Vector3 dir = listener.position - transform.position;
            if(Physics.Raycast(transform.position,dir, dir.magnitude, wall))
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

    private void OnDrawGizmos()
    {
        //float distance = Vector3.Distance(transform.position, listener.transform.position);
        //Gizmos.color = Physics.Raycast(transform.position, listener.transform.position - transform.position, distance, wall) ? Color.red : Color.green;
        //Gizmos.DrawLine(transform.position, listener.transform.position);
    }

}
