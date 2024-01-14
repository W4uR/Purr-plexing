using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public void OnButtonSelected(TextAudio.Phrase phrase)
    {
        Narrator.Instance.Speak(phrase);
    }
}



