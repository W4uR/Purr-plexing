using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ToyTrap : MonoBehaviour
{
    [SerializeField]
    [Range(1f,5f)]
    private float _radius = 2f;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            ScareNearby();
        }
    }

    private void ScareNearby()
    {
        Scareable[] scareables = FindObjectsByType<Scareable>(FindObjectsSortMode.None);
        foreach (Scareable scareable in scareables)
        {
            if (Vector3.Distance(scareable.transform.position, transform.position) <= _radius)
            {
                scareable.transform.SetParent(LevelManager.GetLevelParent());
                scareable.GotScaredFrom(transform.position);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
