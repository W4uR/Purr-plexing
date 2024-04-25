using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreezAudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource _leftBreezeSource;
    [SerializeField]
    private AudioSource _rightBreezeSource;
    [SerializeField]
    private AnimationCurve _volumeFunction;

    private void FixedUpdate()
    {
        HandleBreezeAudio();
        HandleBreezeDisplay();
    }


    private void HandleBreezeAudio()
    {
        float leftBreezeVolume = _volumeFunction.Evaluate(WallDistanceToDirection(RelativeDirection.LEFT));

        if (leftBreezeVolume != 0f)
        {
            _leftBreezeSource.volume = leftBreezeVolume; 
            if (!_leftBreezeSource.isPlaying)
                _leftBreezeSource.Play();
        }
        else
        {
            _leftBreezeSource.Pause();
        }


        float rightBreezeVolume = _volumeFunction.Evaluate(WallDistanceToDirection(RelativeDirection.RIGHT));
        if (rightBreezeVolume != 0f)
        {
            _rightBreezeSource.volume = rightBreezeVolume;
            if (!_rightBreezeSource.isPlaying)
                _rightBreezeSource.Play();
        }
        else
        {
            _rightBreezeSource.Pause();
        }
    }

    private void HandleBreezeDisplay()
    {
        BreezeDisplay.SetLeft(_leftBreezeSource.volume);
        BreezeDisplay.SetRight(_rightBreezeSource.volume);
    }

    public float WallDistanceToDirection(RelativeDirection direction)
    {
        Ray ray;
        switch (direction)
        {
            case RelativeDirection.FORWARD:
                ray = new Ray(_rightBreezeSource.transform.position, transform.forward);
                break;
            case RelativeDirection.RIGHT:
                ray = new Ray(_rightBreezeSource.transform.position, transform.right);
                break;
            case RelativeDirection.BACK:
                ray = new Ray(_rightBreezeSource.transform.position, -transform.forward);
                break;
            case RelativeDirection.LEFT:
            default:
                ray = new Ray(_rightBreezeSource.transform.position, -transform.right);
                break;
        }
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit.distance;

    }
}
