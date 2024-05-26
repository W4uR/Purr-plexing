using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LevelScript());
    }

    IEnumerator LevelScript()
    {
        var trapAudioSource = GameObject.FindAnyObjectByType<SoundTrap>().GetComponent<AudioSource>();

        while (!trapAudioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        yield return Narrator.PlayAudioClip("trap.1");
        yield return new WaitForSeconds(0.4f);
        yield return Narrator.PlayAudioClip("trap.2");
        yield return new WaitForSeconds(0.4f);
        Narrator.Clear();
        yield return null;
    }
}
