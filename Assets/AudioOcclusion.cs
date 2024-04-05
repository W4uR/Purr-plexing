using UnityEngine;

[RequireComponent(typeof(AudioLowPassFilter))]
[RequireComponent(typeof(AudioSource))]
public class AudioOcclusion : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioLowPassFilter lowPassFilter;
    private Player Listener;

    [Header("Occlusion Options")]
    [SerializeField]
    [Range(0f, 1f)]
    private float SoundOcclusionWidening = 0.2f;
    [SerializeField]
    [Range(0f, 1f)]
    private float PlayerOcclusionWidening = 0.2f;
    [SerializeField]
    private LayerMask OcclusionLayer;

    private float lineCastHitCount = 0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lowPassFilter = GetComponent<AudioLowPassFilter>();
        Listener = Player.GetInstance();
    }

    private void FixedUpdate()
    {
        if (!audioSource.isVirtual && audioSource.isPlaying)
            OccludeBetween(transform.position, Listener.transform.position);
        lineCastHitCount = 0f;
    }

    private void OccludeBetween(Vector3 sound, Vector3 listener)
    {
        Vector3 SoundLeft = CalculatePoint(sound, listener, SoundOcclusionWidening, true);
        Vector3 SoundRight = CalculatePoint(sound, listener, SoundOcclusionWidening, false);

        Vector3 ListenerLeft = CalculatePoint(listener, sound, PlayerOcclusionWidening, true);
        Vector3 ListenerRight = CalculatePoint(listener, sound, PlayerOcclusionWidening, false);

        CastLine(SoundLeft, ListenerLeft);
        CastLine(SoundLeft, listener);
        CastLine(SoundLeft, ListenerRight);

        CastLine(sound, ListenerLeft);
        CastLine(sound, listener);
        CastLine(sound, ListenerRight);

        CastLine(SoundRight, ListenerLeft);
        CastLine(SoundRight, listener);
        CastLine(SoundRight, ListenerRight);

        ApplyCalculatedOcclusion();
    }

    private Vector3 CalculatePoint(Vector3 a, Vector3 b, float m, bool posOrneg)
    {
        float x;
        float z;
        float n = Vector3.Distance(new Vector3(a.x, 0f, a.z), new Vector3(b.x, 0f, b.z));
        float mn = (m / n);
        if (posOrneg)
        {
            x = a.x + (mn * (a.z - b.z));
            z = a.z - (mn * (a.x - b.x));
        }
        else
        {
            x = a.x - (mn * (a.z - b.z));
            z = a.z + (mn * (a.x - b.x));
        }
        return new Vector3(x, a.y, z);
    }

    private void CastLine(Vector3 Start, Vector3 End)
    {
        RaycastHit hit;
        Physics.Linecast(Start, End, out hit, OcclusionLayer);

        if (hit.collider)
        {
            lineCastHitCount++;
            Debug.DrawLine(Start, End, Color.red);
        }
        else
            Debug.DrawLine(Start, End, Color.green);
    }

    private void ApplyCalculatedOcclusion()
    {
        audioSource.volume = 1f - 0.8f * lineCastHitCount / 9f;
        lowPassFilter.cutoffFrequency = (-18700f * lineCastHitCount / 9f)+22000f;
    }
}
