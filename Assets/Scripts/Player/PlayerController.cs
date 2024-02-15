using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float stepDuration;
    [SerializeField]
    float rotateDuration;

    [SerializeField]
    PlayerAudioHandler playerAudioHandler;

    [SerializeField]
    LayerMask wallLayers;

    private bool isMoving = false;
    private bool isRotating = false;


    private void OnEnable()
    {
        LevelManager.levelLoaded += TeleportToSpawn;
    }

    private void OnDisable()
    {
        LevelManager.levelLoaded -= TeleportToSpawn;
    }

    void TeleportToSpawn()
    {
        transform.position = LevelManager.GetSpawnPosition();
        transform.rotation = Quaternion.FromToRotation(transform.forward, Vector3.forward);
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (isMoving || isRotating) return;

        // Mozgás elõre 'W' gomb vagy kurzorbillentyû megnyomásakor, ha a mozgás lehetséges
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && IsValidMove())
        {
            StartCoroutine(MoveForward());
        }

        // 90 fok fordulás balra 'A' gomb vagy kurzorbillentyû megnyomásakor
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(Rotate(RelativeDirection.LEFT));
        }

        // 90 fok fordulás jobbra 'D' gomb vagy kurzorbillentyû megnyomásakor
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(Rotate(RelativeDirection.RIGHT));
        }
    }


    bool IsValidMove()
    {
        // Raycast elõre,hogy van-e ott fal
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f, wallLayers, QueryTriggerInteraction.Ignore))
        {
            // A mozgás nem lehetséges
            Debug.Log("Invalid Move: Wall detected! " + hit.transform.name);
            return false;
        }

        // A mozgás lehetséges
        return true;
    }

    IEnumerator MoveForward()
    {
        isMoving = true;

        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + transform.forward;

        Cell currentCell = LevelManager.GetCell(startPosition);
        Cell targetCell = LevelManager.GetCell(endPosition);


        StartCoroutine(playerAudioHandler.PlayStepsForward(currentCell, targetCell));

        while (elapsedTime < stepDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / stepDuration);
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        transform.position = endPosition;

        isMoving = false;
    }

    IEnumerator Rotate(RelativeDirection direction)
    {
        isRotating = true;

        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0f, direction.toRotationAngle(), 0f) * startRotation;

        Cell currentCell = LevelManager.GetCell(transform.position);
        StartCoroutine(playerAudioHandler.PlayRotationSteps(currentCell,direction));

        while (elapsedTime < rotateDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / rotateDuration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame(); // Wait for the next frame
        }
        transform.rotation = endRotation;

        isRotating = false;
    }
}
