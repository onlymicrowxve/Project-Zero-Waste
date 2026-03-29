using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Riferimenti UI")]
    public GameObject mainMenuPanel;

    void Start()
    {
        mainMenuPanel.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Interno Casa");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}