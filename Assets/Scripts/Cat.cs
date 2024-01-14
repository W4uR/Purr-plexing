using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField]
    CustomAudioSource audioSource;

    [SerializeField]
    AudioSource purringSource;

    private bool isCarried = false;

    public void SetCarried(bool carried)
    {
        isCarried = carried;
        if (isCarried)
        {
            purringSource.Stop();
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

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, .64f, 0f);
        Gizmos.DrawWireSphere(transform.position, .3f);
    }
}
