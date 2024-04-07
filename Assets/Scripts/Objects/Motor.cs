using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using static Unity.Burst.Intrinsics.X86;

public class Motor : MonoBehaviour
{
    [SerializeField]
    private float stepDuration = 1f;
    [SerializeField]
    private float turnDuration = 1f;
    [SerializeField]
    private LayerMask wallLayers;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private bool playStepSounds = false;

    private bool isMoving = false;

    public float GetStepDuration() => stepDuration;

    public bool IsValidMove(Vector3 endPosition)
    {
        if (Physics.Linecast(transform.position, endPosition, wallLayers, QueryTriggerInteraction.Ignore))
        {
            // A mozgás nem lehetséges
            Debug.Log("Invalid Move: Wall detected!");
            return false;
        }
        // A mozgás lehetséges
        return true;
    }

    public void OnMoveForward(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
            MoveToward(RelativeDirection.FORWARD);
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        if(context.performed)
            TurnToward(context.ReadValue<float>() < 0f ? RelativeDirection.LEFT : RelativeDirection.RIGHT);
    }

    public void MoveToward(AbsoluteDirection direction)
    {
        if (isMoving) return;
        Vector3 endPosition = transform.position + direction.ToVector3();
        if (!IsValidMove(endPosition)) return;
        StartCoroutine(MoveOverTime(endPosition));
        if (playStepSounds)
            StartCoroutine(PlayStepsMoving(LevelManager.CurrentLevel.GetCell(transform.position), LevelManager.CurrentLevel.GetCell(endPosition)));
    }

    public void MoveToward(RelativeDirection direction)
    {
        if (isMoving) return;
        Vector3 endPosition = transform.position + direction.ToVector3(transform);
        if (!IsValidMove(endPosition)) return;
        StartCoroutine(MoveOverTime(endPosition));
        if (playStepSounds)
            StartCoroutine(PlayStepsMoving(LevelManager.CurrentLevel.GetCell(transform.position), LevelManager.CurrentLevel.GetCell(endPosition)));
    }

    private IEnumerator MoveOverTime(Vector3 endPosition)
    {
        isMoving = true;
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        if(playStepSounds)

        while (elapsedTime < stepDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / stepDuration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = endPosition;
        isMoving = false;
    }

    public void TurnToward(RelativeDirection direction)
    {
        if (isMoving) return;
        Quaternion endRotation = Quaternion.Euler(0f, direction.toRotationAngle(), 0f) * transform.rotation;
        StartCoroutine(TurnOverTime(endRotation));
        if(playStepSounds)
            StartCoroutine(PlayStepsTurning(LevelManager.CurrentLevel.GetCell(transform.position),direction));
    }

    private IEnumerator TurnOverTime(Quaternion endRotation)
    {
        isMoving = true;
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        while (elapsedTime < turnDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / turnDuration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.rotation = endRotation;
        isMoving = false;
    }

    private IEnumerator PlayStepsMoving(Cell startingCell, Cell targetCell)
    {
        audioSource.PlayOneShot(startingCell.GetStepSounds().GetRandomClip());
         yield return new WaitForSeconds(stepDuration*.5f);
        audioSource.PlayOneShot(targetCell.GetStepSounds().GetRandomClip());
    }

    private IEnumerator PlayStepsTurning(Cell currentCell, RelativeDirection turningDirection)
    {
        audioSource.panStereo = turningDirection.ToPanStereo();
        audioSource.PlayOneShot(currentCell.GetStepSounds().GetRandomClip());
        yield return new WaitForSeconds(turnDuration*.7f);
        audioSource.panStereo = 0f;
        audioSource.PlayOneShot(currentCell.GetStepSounds().GetRandomClip());
    }
}
