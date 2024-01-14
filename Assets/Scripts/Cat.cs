using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField]
    CustomAudioSource audioSource;

    public bool isCarried = false;


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
       // float distanceFromCaller = Vector3.Distance(callerPos, transform.position);
        StartCoroutine(GiveSoundWithRandomDelay());
    }


    IEnumerator GiveSoundWithRandomDelay()
    {
        yield return new WaitForSeconds(Random.Range(1f, 2.3f));
        if (isCarried == false)
        {
            audioSource.PlayRandomFromGroup("meows", true);
        }
        else
        {
            audioSource.PlayRandomFromGroup("purrs", false);
        }
    }
}
