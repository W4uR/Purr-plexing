using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Introduction());
    }

    
    IEnumerator Introduction()
    {
        // Welcome player
        for (int i = 1; i <= 5; i++)
        {
            yield return Narrator.PlayAudioClip("tutorial.intro." + i);
        }

        // Introduce cats and cat calling
        yield return Narrator.PlayAudioClip("tutorial.controls.calling.1");
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
        yield return Task.Delay(TimeSpan.FromSeconds(2));
        yield return Narrator.PlayAudioClip("tutorial.cat.response");

        // Explain movement
        yield return Narrator.PlayAudioClip("tutorial.controls.movement.forward");
        while (!Input.GetKeyDown(KeyCode.W))
        {
            yield return null;
        }

        // Explain footstep sounds ("Different floors can have different sounds use this infromation to better anvigate through the level")
        yield return Narrator.PlayAudioClip("tutorial.cues.steps");
        
        // Wait for breeze in right ear
        while(Physics.Raycast(Player.GetPosition(),Player.GetInstance().transform.right,1f))
        {
            yield return null;
        }
        // Introduce breeze sfx
        yield return Narrator.PlayAudioClip("tutorial.cues.breeze");
        // Introduce turning
        yield return Narrator.PlayAudioClip("tutorial.controls.movement.turning");
        while (!Input.GetKeyDown(KeyCode.D))
        {
            yield return null;
        }
        yield return Narrator.PlayAudioClip("tutorial.cues.breeze.volume");

        // Try calling for the cat again (now its much louder and can be heard coming from front of the player)
        yield return Narrator.PlayAudioClip("tutorial.controls.calling.2");
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
        yield return Narrator.PlayAudioClip("tutorial.cues.occlusion");
        
        

    }
    
}
