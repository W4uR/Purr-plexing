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

        // Mozg�s el�re 'W' gomb vagy kurzorbillenty� megnyom�sakor, ha a mozg�s lehets�ges
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerMover.MoveToward(RelativeDirection.FORWARD);
        }

        // 90 fok fordul�s balra 'A' gomb vagy kurzorbillenty� megnyom�sakor
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerMover.TurnToward(RelativeDirection.LEFT);
        }

        // 90 fok fordul�s jobbra 'D' gomb vagy kurzorbillenty� megnyom�sakor
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerMover.TurnToward(RelativeDirection.RIGHT);
        }
    }

}
