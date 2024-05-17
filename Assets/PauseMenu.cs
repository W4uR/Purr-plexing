using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    [SerializeField]
    Button backToMainButton;

    private void Awake()
    {
        backToMainButton.onClick.AddListener(delegate { SceneManager.LoadScene(0); });
    }

}
