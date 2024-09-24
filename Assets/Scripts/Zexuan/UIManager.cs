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
    public Tools tools;
    public Button SettingsButton;
    public GameObject fadeImage;
    public GameObject pauseMenu;
    public GameObject plantingToolUI;
    public GameObject wateringToolUI;
    public GameObject[] equapmentBar;
    public bool isTransitioning;


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
        GetComponent<SceneTransition>().LoadScene("GameScene");
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
        foreach (GameObject item in equapmentBar)
        {
            item.SetActive(false);
        }   
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        foreach (GameObject item in equapmentBar)
        {
            item.SetActive(true);
        }
        
    }

    public void ReturnMainMenu()
    {
        fadeImage.SetActive(true);
        GetComponent<SceneTransition>().LoadScene("MainMenu");
    }

    public void SwitchToPlantingTool()
    {
        tools.isPlantingTool = true;
        tools.isWateringTool = false;
        plantingToolUI.SetActive(true);
        wateringToolUI.SetActive(false);
    }

    public void SwitchToWateringTool()
    {
        tools.isPlantingTool = false;
        tools.isWateringTool = true;
        plantingToolUI.SetActive(false);
        wateringToolUI.SetActive(true);
    }
}
