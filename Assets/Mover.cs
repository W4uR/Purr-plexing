using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    float stepDuration = 1f;

    private bool isMoving = false;

    public IEnumerator MoveOverTime(AbsoluteDirection direction)
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + direction.ToVector3();

        while (elapsedTime < stepDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / stepDuration);
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        transform.position = endPosition;

        isMoving = false;
    }
}
