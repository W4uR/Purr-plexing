using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RandomSoundPlayer))]
public class CatCaller : MonoBehaviour
{
    [SerializeField]
    [Range(0.5f, 5f)]
    private float whislteCooldown = 1f;
    [SerializeField]
    AudioGroup whistles;

    RandomSoundPlayer randomSoundPlayer;
    private bool canWhistle = true;
    public static event Action<Vector3> CallingCats;

    private void Start()
    {
        randomSoundPlayer = GetComponent<RandomSoundPlayer>();
    }


    public void OnWhistle(InputAction.CallbackContext context)
    {
        if (!canWhistle) return;
        StartCoroutine(nameof(Whistle));
    }

    private IEnumerator Whistle()
    {
        canWhistle = false;
        randomSoundPlayer.PlayRandomFromGroup(whistles);
        CallingCats.Invoke(transform.position);
        yield return new WaitForSeconds(whislteCooldown);
        canWhistle = true;
    }


}
