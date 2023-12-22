using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField]
    AudioClip[] meows;

    [SerializeField]
    AudioSource audioSource;


    bool isCarried = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PlayRandomMeow", 0, 5);
    }

    void PlayRandomMeow()
    {
        if(isCarried == false)
        {
            int randomIndex = Random.Range(0, meows.Length);
            audioSource.PlayOneShot(meows[randomIndex]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCarried = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCarried = false;
        }
    }
}
