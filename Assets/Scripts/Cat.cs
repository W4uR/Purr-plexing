using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField]
    RandomSoundPlayer randomSoundPlayer;
    [SerializeField]
    AudioSource passivePurrer;

    private bool isCarried = false;

    public void SetCarried(bool carried)
    {
        isCarried = carried;
        if (isCarried)
        {
            passivePurrer.Stop();
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
            randomSoundPlayer.PlayRandomFromGroup("meows");
        }
        else
        {
            randomSoundPlayer.PlayRandomFromGroup("purrs");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, .64f, 0f);
        Gizmos.DrawWireSphere(transform.position, .3f);
    }
}
