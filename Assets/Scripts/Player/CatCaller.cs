using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAudioHandler))]
public class CatCaller : MonoBehaviour
{
    [SerializeField]
    [Range(0.5f, 5f)]
    private float whislteCooldown = 1f;

    private bool canWhistle = true;

    public static event Action<Vector3> CallingCats;

    private void Update()
    {
        if (!canWhistle) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(nameof(Whistle));
        }
    }

    private IEnumerator Whistle()
    {
        canWhistle = false;
        GlobalSoundEffects.Instance.PlayWhistle();
        CallingCats.Invoke(transform.position);
        yield return new WaitForSeconds(whislteCooldown);
        canWhistle = true;
    }
}
