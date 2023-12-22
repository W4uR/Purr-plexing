using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    public IEnumerator PlayStepsForward(Cell currentCell, Cell targetCell)
    {
        AudioClip step1 = currentCell.getRandomStepSound();
        AudioClip step2 = targetCell.getRandomStepSound();
        audioSource.PlayOneShot(step1);
        yield return new WaitForSeconds(step1.length);
        audioSource.PlayOneShot(step2);
    }

    public IEnumerator PlayRotationSteps(Cell currentCell, RelativeDirection newForward)
    {
        AudioClip step1 = currentCell.getRandomStepSound();
        AudioClip step2 = currentCell.getRandomStepSound();
        audioSource.panStereo = newForward.toStereo();
        audioSource.PlayOneShot(step1);
        yield return new WaitForSeconds(0.3f);
        audioSource.panStereo = 0f;
        audioSource.PlayOneShot(step2);
        audioSource.pitch = 1f;
    }

}
