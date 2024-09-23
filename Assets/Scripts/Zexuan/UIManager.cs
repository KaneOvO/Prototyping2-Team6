using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //singleton
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI toolText;
    public Tools tools;
    public Button SettingsButton;
    public GameObject fadeImage;
    public GameObject pauseMenu;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }


    public void UpdateToolText(string toolName)
    {
        if(toolText != null)
        {
            if(tools.isPlantingTool)
            {
                toolText.text = "Current Tool: " + toolName;
            }
            else if(tools.isWateringTool)
            {
                toolText.text = "Current Tool: " + toolName;
            }
        }
    }

    public void StartGame()
    {
        fadeImage.SetActive(true);
        GetComponent<SceneTransition>().LoadScene("GameScene");
    }

    public void RestartGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        Time.timeScale = 1;
        fadeImage.SetActive(true);
        SceneManager.LoadScene("GameScene");
    }

    public void Credits()
    {
        fadeImage.SetActive(true);
        //SceneManager.LoadScene("Credits");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;   
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        
    }

    public void ReturnMainMenu()
    {
        fadeImage.SetActive(true);
        GetComponent<SceneTransition>().LoadScene("MainMenu");
    }
}
