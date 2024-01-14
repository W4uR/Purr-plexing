using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{
    [Header("Steps")]
    [SerializeField]
    AudioSource stepsSource;

    [Header("Breeze")]
    [SerializeField]
    AudioSource LeftBreezeSource;
    [SerializeField]
    AudioSource RightBreezeSource;
    [Range(1f, 50f)]
    [SerializeField]
    float breezeVolumeScale = 1f;


    private void FixedUpdate()
    {
        HandleBreezeAudio();
    }

    void HandleBreezeAudio()
    {
        // Jobb fül
        Ray ray = new Ray(transform.position, transform.right);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            RightBreezeSource.volume = hit.distance / breezeVolumeScale;
            if(hit.distance > 1f)
            {
                if (!RightBreezeSource.isPlaying)
                    RightBreezeSource.Play();
            }
            else
            {
                RightBreezeSource.Pause();
            }
        }

        // Bal fül
        ray = new Ray(transform.position, -transform.right);
        if (Physics.Raycast(ray, out hit))
        {
            LeftBreezeSource.volume = hit.distance / breezeVolumeScale;
            if (hit.distance > 1f)
            {
                if (!LeftBreezeSource.isPlaying)
                    LeftBreezeSource.Play();
            }
            else
            {
                LeftBreezeSource.Pause();
            }
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
