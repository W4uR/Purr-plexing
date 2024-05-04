using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatWhistler : MonoBehaviour
{
    [SerializeField]
    [Range(0.5f, 5f)]
    private float whislteCooldown = 1f;
    [SerializeField]
    private AudioGroup whistles;
    [SerializeField]
    AudioSource audioSource;

    private bool canWhistle = true;
    public static event Action<Vector3> CallingCats;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnWhistle(InputAction.CallbackContext context)
    {
        if (!canWhistle) return;
        StartCoroutine(nameof(Whistle));
    }

    private IEnumerator Whistle()
    {
        canWhistle = false;
        audioSource.PlayOneShot(whistles.GetRandomClip());
        CallingCats.Invoke(transform.position);
        yield return new WaitForSeconds(whislteCooldown);
        canWhistle = true;
    }

}
