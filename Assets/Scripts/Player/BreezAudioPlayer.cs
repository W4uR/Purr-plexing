using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreezAudioPlayer : MonoBehaviour
{
    [SerializeField]
    AudioSource LeftBreezeSource;
    [SerializeField]
    AudioSource RightBreezeSource;
    [SerializeField]
    AnimationCurve volumeFunction;

    private void FixedUpdate()
    {
        HandleBreezeAudio();
    }

    void HandleBreezeAudio()
    {
        float leftBreezeVolume = volumeFunction.Evaluate(DistanceToLeftWall());

        if (leftBreezeVolume != 0f)
        {
            LeftBreezeSource.volume = leftBreezeVolume;
            if (!LeftBreezeSource.isPlaying)
                LeftBreezeSource.Play();
        }
        else
        {
            LeftBreezeSource.Pause();
        }


        float rightBreezeVolume = volumeFunction.Evaluate(DistanceToRightWall());
        if (rightBreezeVolume != 0f)
        {
            RightBreezeSource.volume = rightBreezeVolume;
            if (!RightBreezeSource.isPlaying)
                RightBreezeSource.Play();
        }
        else
        {
            RightBreezeSource.Pause();
        }
    }

    public float DistanceToLeftWall()
    {
        Ray ray = new Ray(LeftBreezeSource.transform.position, -transform.right);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit.distance;
    }

    public float DistanceToRightWall()
    {
        Ray ray = new Ray(RightBreezeSource.transform.position, transform.right);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit.distance;
    }
}
