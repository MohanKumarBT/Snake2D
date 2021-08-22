using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public GameObject pauseUI, gameOverUI;
    public string currentScene, MenuScene;

    private bool isPaused;

    public void PauseButtonClick()
    {
        Time.timeScale = 0f;
        pauseUI.gameObject.SetActive(true);

    }

    public void ResumeButtonClick()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseUI.gameObject.SetActive(false);
    }
    public void RestartButtonClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentScene);
    }
    public void MenuButtonClick()
    {
        SceneManager.LoadScene(MenuScene);
    }

   
}
