using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField]
    AudioGroup meows;
    [SerializeField]
    AudioGroup purrs;

    [SerializeField]
    AudioSource meowSource;
    [SerializeField]
    AudioSource purrsSource;

    private bool isCarried = false;

    public void SetCarried(bool carried)
    {
        isCarried = carried;
        if (isCarried)
        {
            purrsSource.Stop();
        }
    }

    private void OnEnable()
    {
        CatCaller.CallingCats += OnCallingCats;
    }

    private void OnDisable()
    {
        CatCaller.CallingCats -= OnCallingCats;
    }

    private void OnCallingCats(Vector3 callerPos)
    {
        
        StartCoroutine(GiveSoundWithRandomDelay());
    }


    IEnumerator GiveSoundWithRandomDelay()
    {
        yield return new WaitForSeconds(Random.Range(0.3f, 1.9f));
        if (isCarried == false)
        {
            meowSource.PlayOneShot(meows.GetRandomClip());
        }
        else
        {
            meowSource.PlayOneShot(purrs.GetRandomClip());
        }
    }

}
