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
    private AudioClip bumpSfx;
    [SerializeField]
    private AudioSource _feetAudioSource;

    private bool isMoving = false;


    public void OnMoveForward(InputAction.CallbackContext context)
    {
        if (!context.ReadValueAsButton()) return;
        if (isMoving || GamePauser.IsPaused) return;

        Vector3 endPosition = transform.position + transform.forward;

        if (LevelManager.CurrentLevel.GetCell(endPosition) == null)
        {
            _feetAudioSource.PlayOneShot(bumpSfx);
            return;
        }

        StartCoroutine(MoveOverTime(endPosition));
        StartCoroutine(PlayStepsMoving(LevelManager.CurrentLevel.GetCell(transform.position), LevelManager.CurrentLevel.GetCell(endPosition)));
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (isMoving || GamePauser.IsPaused) return;

        RelativeDirection direction = context.ReadValue<float>() < 0f ? RelativeDirection.LEFT : RelativeDirection.RIGHT;
        Quaternion endRotation = Quaternion.Euler(0f, direction.toRotationAngle(), 0f) * transform.rotation;
        StartCoroutine(TurnOverTime(endRotation));
        StartCoroutine(PlayStepsTurning(LevelManager.CurrentLevel.GetCell(transform.position), direction));
    }


    private IEnumerator MoveOverTime(Vector3 endPosition)
    {
        isMoving = true;
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < _stepDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / _stepDuration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        //Enélkül a koordináta nagyon közeli lehet egy egész számhoz, de nem egész (0-nál jött elõ a Z koordináta esetében) És emiatt nem lehetett hallani a macskát
        // A mozgást lehet újra kéne gondolni és a végpoziciót, nem így meghatározni
        transform.position = endPosition.RoundXZ(); 
        isMoving = false;
    }

    private IEnumerator TurnOverTime(Quaternion endRotation)
    {
        isMoving = true;
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        while (elapsedTime < _turnDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / _turnDuration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.rotation = endRotation;
        isMoving = false;
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
