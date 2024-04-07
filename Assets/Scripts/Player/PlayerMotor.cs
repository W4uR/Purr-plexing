using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{

    [SerializeField]
    public float _stepDuration;
    [SerializeField]
    public float _turnDuration;
    [SerializeField]
    private LayerMask _wallLayermask;
    [SerializeField]
    private AudioSource _feetAudioSource;

    private bool _isMoving = false;


    public void OnMoveForward(InputAction.CallbackContext context)
    {
        if (!context.ReadValueAsButton()) return;
        if (_isMoving) return;

        Vector3 endPosition = transform.position + transform.forward;

        if (LevelManager.CurrentLevel.GetCell(endPosition) == null) return;

        StartCoroutine(MoveOverTime(endPosition));
        StartCoroutine(PlayStepsMoving(LevelManager.CurrentLevel.GetCell(transform.position), LevelManager.CurrentLevel.GetCell(endPosition)));
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (_isMoving) return;

        RelativeDirection direction = context.ReadValue<float>() < 0f ? RelativeDirection.LEFT : RelativeDirection.RIGHT;
        Quaternion endRotation = Quaternion.Euler(0f, direction.toRotationAngle(), 0f) * transform.rotation;
        StartCoroutine(TurnOverTime(endRotation));
        StartCoroutine(PlayStepsTurning(LevelManager.CurrentLevel.GetCell(transform.position), direction));
    }


    private IEnumerator MoveOverTime(Vector3 endPosition)
    {
        _isMoving = true;
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < _stepDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / _stepDuration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = endPosition;
        _isMoving = false;
    }

    private IEnumerator TurnOverTime(Quaternion endRotation)
    {
        _isMoving = true;
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        while (elapsedTime < _turnDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / _turnDuration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.rotation = endRotation;
        _isMoving = false;
    }

    private IEnumerator PlayStepsMoving(Cell startingCell, Cell targetCell)
    {
        _feetAudioSource.PlayOneShot(startingCell.GetStepSounds().GetRandomClip());
         yield return new WaitForSeconds(_stepDuration*.5f);
        _feetAudioSource.PlayOneShot(targetCell.GetStepSounds().GetRandomClip());
    }

    private IEnumerator PlayStepsTurning(Cell currentCell, RelativeDirection turningDirection)
    {
        _feetAudioSource.panStereo = turningDirection.ToPanStereo();
        _feetAudioSource.PlayOneShot(currentCell.GetStepSounds().GetRandomClip());
        yield return new WaitForSeconds(_turnDuration*.7f);
        _feetAudioSource.panStereo = 0f;
        _feetAudioSource.PlayOneShot(currentCell.GetStepSounds().GetRandomClip());
    }
}
