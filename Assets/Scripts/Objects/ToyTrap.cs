using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ToyTrap : MonoBehaviour
{
    [SerializeField]
    [Range(1f,5f)]
    private float radius = 2f;

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            ScareCats();
        }
    }

    private void ScareCats()
    {
        Scareable[] scareables = FindObjectsByType<Scareable>(FindObjectsSortMode.None);
        foreach (Scareable scareable in scareables)
        {
            if (Vector3.Distance(scareable.transform.position, transform.position) <= radius)
            {
                scareable.transform.SetParent(LevelManager.GetLevelParent());
                StartCoroutine(scareable.GotScaredFrom(transform.position));
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
