using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField]
    AudioGroup meowsSFX;
    [SerializeField]
    AudioGroup purrsSFX;

    [SerializeField]
    AudioSource audioSource;

    private bool _isCarried = false;

    private void OnEnable()
    {
        CatWhistler.CallingCats += OnCallingCats;
    }

    private void OnDisable()
    {
        CatWhistler.CallingCats -= OnCallingCats;
    }

    private void OnCallingCats(Vector3 callerPos)
    {
        
        StartCoroutine(GiveSoundWithRandomDelay());
    }


    IEnumerator GiveSoundWithRandomDelay()
    {
        yield return new WaitForSeconds(Random.Range(0.3f, 1.9f));
        if (_isCarried)
        {
            audioSource.PlayOneShot(purrsSFX.GetRandomClip());
        }
        else
        {
            audioSource.PlayOneShot(meowsSFX.GetRandomClip());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CatCarrier>())
        {
            audioSource.Stop();
            _isCarried = true;
        }
    }
}
