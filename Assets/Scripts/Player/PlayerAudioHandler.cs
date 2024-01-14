using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{
    [SerializeField]
    AudioSource stepsSource;
    [SerializeField]
    AudioSource windSource;


    private void FixedUpdate()
    {
        HandleWindSound();
    }

    void HandleWindSound()
    {
        windSource.panStereo = 0f;
        bool shouldPlay = false;


        if (!Physics.Raycast(windSource.transform.position, windSource.transform.right, 1f))
        {
            windSource.panStereo += 1f;
            shouldPlay = true;
        }
        if(!Physics.Raycast(windSource.transform.position, -windSource.transform.right, 1f))
        {
            windSource.panStereo -= 1f;
            shouldPlay = true;
        }
        
        if(shouldPlay && windSource.isPlaying == false)
        {
            windSource.Play();
        }
        else if(shouldPlay == false && windSource.isPlaying)
        {
            windSource.Pause();
        }

    }

    public IEnumerator PlayStepsForward(Cell currentCell, Cell targetCell)
    {
        AudioClip step1 = currentCell.GetRandomStepSound();
        AudioClip step2 = targetCell.GetRandomStepSound();
        stepsSource.PlayOneShot(step1);
        yield return new WaitForSeconds(step1.length);
        stepsSource.PlayOneShot(step2);
    }

    public IEnumerator PlayRotationSteps(Cell currentCell, RelativeDirection newForward)
    {
        AudioClip step1 = currentCell.GetRandomStepSound();
        AudioClip step2 = currentCell.GetRandomStepSound();
        stepsSource.panStereo = newForward.toStereo();
        stepsSource.PlayOneShot(step1);
        yield return new WaitForSeconds(0.3f);
        stepsSource.panStereo = 0f;
        stepsSource.PlayOneShot(step2);
    }
}
