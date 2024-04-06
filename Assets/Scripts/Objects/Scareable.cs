using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Scareable : MonoBehaviour
{
    [SerializeField]
    private int scareFactor = 2;

    [Header("Sounds")]
    [SerializeField]
    AudioGroup scaredSFXGroup;
    [SerializeField]
    RandomSoundPlayer randomSoundPlayer;

    int stepsAvaliable = 0;
    Mover mover;

    private void Start()
    {
        mover = GetComponent<Mover>();
    }

    public IEnumerator GotScaredFrom(Vector3 source)
    {
        stepsAvaliable = scareFactor;
        randomSoundPlayer.PlayRandomFromGroup(scaredSFXGroup);
        while(stepsAvaliable > 0)
        {
            yield return StartCoroutine(FleeFrom(source));
        }
        
    }

    IEnumerator FleeFrom(Vector3 source)
    {
        Predicate<AbsoluteDirection> facingSource = (AbsoluteDirection direction) => Vector3.Dot(transform.position-source, direction.ToVector3())<0f;
        Predicate<AbsoluteDirection> invalidDirection = (AbsoluteDirection direction) => !mover.IsValidMove(transform.position + direction.ToVector3()); 
        List<AbsoluteDirection> posibleRoutes = ((AbsoluteDirection[])Enum.GetValues(typeof(AbsoluteDirection))).ToList();
        //posibleRoutes.Where(direction => Vector3.Dot(transform.position - source, direction.ToVector3()) < 0f).ToList().ForEach(x => Debug.Log("Removed: " + x.HumanName()));
        posibleRoutes.RemoveAll(invalidDirection);
        posibleRoutes.RemoveAll(facingSource);
        if (posibleRoutes.Count == 0)
        {
            stepsAvaliable = 0;
            yield break;
        }
        //posibleRoutes.ForEach(x => Debug.Log(stepsAvaliable + ": "+x.HumanName()));
        int randomFleeDirectionIndex = UnityEngine.Random.Range(0, posibleRoutes.Count);
        mover.MoveToward(posibleRoutes[randomFleeDirectionIndex]);
        yield return new WaitForSeconds(mover.GetStepDuration());
        stepsAvaliable--;
    }
}
