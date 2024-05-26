using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionMenu : MonoBehaviour
{
    [SerializeField]
    List<Button> levelButtons;

    private void Awake()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            int index = i; // Capture the current value of i
            levelButtons[i].onClick.AddListener(() => OnButtonClick(index));
            if (i>PlayerPrefs.GetInt(Constants.UNLOCKED_LEVELS))
            {
                levelButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void OnButtonClick(int index)
    {
        GameManager.Instance.StartGame(index);
    }
}
