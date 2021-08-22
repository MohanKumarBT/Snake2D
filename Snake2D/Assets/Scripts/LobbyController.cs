using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    //public string sceneName;
    [SerializeField] private GameObject InstructionPanel;
    [SerializeField] private GameObject LobbyPanel;

    private void Awake()
    {
        LobbyPanel.SetActive(true);
        InstructionPanel.SetActive(false);
    }
    public void Play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }
    public void InstructionBtn()
    {
        LobbyPanel.SetActive(false);
        InstructionPanel.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Backbtn()
    {
        LobbyPanel.SetActive(true);

        InstructionPanel.SetActive(false);
    }
}
