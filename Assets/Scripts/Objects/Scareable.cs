using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class Scareable : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    private int _stepsAvaliable = 2;

    [Header("Sounds")]
    [SerializeField]
    private AudioGroup _scaredSFXGroup;
    [SerializeField]
    private AudioSource _audioSource;

    private int _stepsLeft = 0;

    public void GotScaredFrom(Vector3 source)
    {
        _stepsLeft = _stepsAvaliable;
        _audioSource.PlayOneShot(_scaredSFXGroup.GetRandomClip());
        FleeFrom(source);
    }

    void FleeFrom(Vector3 source)
    {
        if (_stepsLeft == 0) return;

        bool facingSource(AbsoluteDirection direction) => Vector3.Dot(transform.position - source, direction.ToVector3()) < 0f;
        bool invalidMove(AbsoluteDirection direction) => LevelManager.CurrentLevel.GetCell(transform.position + direction.ToVector3()) == null;

        var posibleRoutes = ((AbsoluteDirection[])Enum.GetValues(typeof(AbsoluteDirection))).
            Where(direction => !facingSource(direction) && !invalidMove(direction)).ToList();

        if (posibleRoutes.Count() == 0)
        {
            _stepsLeft = 0;
            return;
        }

        Vector3 oldPosition = transform.position;
        int randomFleeDirectionIndex = UnityEngine.Random.Range(0, posibleRoutes.Count);
        transform.Translate(posibleRoutes[randomFleeDirectionIndex].ToVector3(),Space.World);
        _stepsLeft--;
        FleeFrom(oldPosition);
    }
}
