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

    private static PlayerAudioHandler instance;

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        HandleBreezeAudio();
    }

    void HandleBreezeAudio()
    {
        float leftBreezeVolume = DistanceToVolume(DistanceToLeftWall());

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


        float rightBreezeVolume = DistanceToVolume(DistanceToRightWall());
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

    // Statikus metódusok

    public static Vector3 GetRightEarPosition()
    {
        return instance.transform.position + instance.transform.right * 0.2f;
    }
    public static Vector3 GetLefttEarPosition()
    {
        return instance.transform.position - instance.transform.right * 0.2f;
    }

    private static float DistanceToVolume(float distance)
    {
        if (distance < 1f)
        {
            return 0f;
        }
        else if (distance < 3f)
        {
            return 0.12f;
        }
        else if (distance < 5f)
        {
            return 0.46f;
        }
        else
        {
            return 65f;
        }
    }

    public static float DistanceToLeftWall()
    {
        Ray ray = new Ray(instance.transform.position, -instance.transform.right);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit.distance;
    }

    public static float DistanceToRightWall()
    {
        Ray ray = new Ray(instance.transform.position, instance.transform.right);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit.distance;
    }
}
