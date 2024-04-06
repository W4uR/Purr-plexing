using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Mover playerMover;


    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {

        // Mozgás elõre 'W' gomb vagy kurzorbillentyû megnyomásakor, ha a mozgás lehetséges
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerMover.MoveToward(RelativeDirection.FORWARD);
        }

        // 90 fok fordulás balra 'A' gomb vagy kurzorbillentyû megnyomásakor
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerMover.TurnToward(RelativeDirection.LEFT);
        }

        // 90 fok fordulás jobbra 'D' gomb vagy kurzorbillentyû megnyomásakor
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerMover.TurnToward(RelativeDirection.RIGHT);
        }
    }

}
