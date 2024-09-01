using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleteMenu : MonoBehaviour
{
    [SerializeField]
    Button nextLevelButton;
    [SerializeField]
    Button backToMainButton;

    private void Awake()
    {
        nextLevelButton.onClick.AddListener(delegate { LevelManager.LoadNextLevel(); });
        backToMainButton.onClick.AddListener(delegate { SceneManager.LoadScene(0); });
    }
}
