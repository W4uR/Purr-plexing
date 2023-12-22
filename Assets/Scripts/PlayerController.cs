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

    private bool isMoving = false;
    private bool isRotating = false;

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (isMoving || isRotating) return;

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && IsValidMove())
        {
            StartCoroutine(MoveForward());
        }

        // Rotate 90 degrees to the left on 'A' key press
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(Rotate(-90f));
        }

        // Rotate 90 degrees to the right on 'D' key press
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(Rotate(90f));
        }
    }


    bool IsValidMove()
    {
        // Cast a ray forward to check for obstacles
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
        {
            // If the ray hits something, movement is not valid
            Debug.Log("Invalid Move: Obstacle detected!");
            return false;
        }

        // Movement is valid if no obstacle is detected
        return true;
    }

    IEnumerator MoveForward()
    {
        isMoving = true;

        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + transform.forward;
        Cell currentCell = LevelManager.getCell(transform.position);
        Cell targetCell = LevelManager.getCell(transform.position+transform.forward);


        StartCoroutine(playerAudioHandler.PlayStepsForward(currentCell, targetCell));

        while (elapsedTime < stepDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / stepDuration);
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame(); // Wait for the next frame
        }

        transform.position = endPosition;

        isMoving = false;
    }

    IEnumerator Rotate(float targetAngle)
    {
        isRotating = true;

        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0f, targetAngle, 0f) * startRotation;

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
