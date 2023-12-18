using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isMoving = false;
    private bool isRotating = false;

    void Update()
    {
        // Check for input only if the object is not already moving or rotating
        if (!isMoving && !isRotating)
        {
            // Move forward on 'W' key press
            if (Input.GetKeyDown(KeyCode.W) && IsValidMove())
            {
                StartCoroutine(MoveObjectForward());
            }

            // Rotate 90 degrees to the left on 'A' key press
            if (Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(RotateObject(-90f));
            }

            // Rotate 90 degrees to the right on 'D' key press
            if (Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(RotateObject(90f));
            }
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

    IEnumerator MoveObjectForward()
    {
        isMoving = true;

        float elapsedTime = 0f;
        float duration = 1f;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + transform.forward;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        transform.position = endPosition;

        isMoving = false;
    }

    IEnumerator RotateObject(float targetAngle)
    {
        isRotating = true;

        float elapsedTime = 0f;
        float duration = 1f; // You can adjust the rotation speed as needed
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0f, targetAngle, 0f) * startRotation;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        transform.rotation = endRotation;

        isRotating = false;
    }
}
