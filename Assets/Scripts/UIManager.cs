using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    

    public void OnStartGameClicked()
    {
        GameManager.Instance.StartGame();
    }
     public void OnQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }
}
