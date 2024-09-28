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
    public ToolsManager tools;
    public Button SettingsButton;
    public GameObject StartButton;
    public GameObject CreditsButton;
    public GameObject ExitButton;
    public GameObject fadeImage;
    public GameObject creditsPanel;
    public GameObject pauseMenu;
    public GameObject plantingToolUI;
    public GameObject wateringToolUI;
    public GameObject MiniMapMask;
    public GameObject MiniMap;
    public Slider volumeSlider;
    public GameObject[] equapmentBar;
    public bool isMainMenu;
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

    void Start()
    {
        if (volumeSlider != null && AudioManager.Instance != null)
        {
            volumeSlider.value = AudioManager.Instance.globalVolume;
        }

        if(!SceneManager1.Instance.isBackToMainMenu)
        {
            isMainMenu = false;
            ShowGameUI();
            HiddenStartUI();
        }
        else
        {
            isMainMenu = true;
            ShowStartUI();
            HiddenGameUI();
        }
    }

    public void StartGame()
    {
        LockCursor();
        isMainMenu = false;
        ShowGameUI();
        HiddenStartUI();
        GameManager.Instance.isThirdPesronView = true;
        GameManager.Instance.isWorldView = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        LockCursor();
        SceneManager1.Instance.isBackToMainMenu = false;
        GetComponent<SceneTransition>().LoadScene("GameScene");
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
        StartButton.SetActive(false);
        CreditsButton.SetActive(false);
        ExitButton.SetActive(false);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        StartButton.SetActive(true);
        CreditsButton.SetActive(true);
        ExitButton.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        UnlockCursor();
        // foreach (GameObject item in equapmentBar)
        // {
        //     item.SetActive(false);
        // }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        LockCursor();
        // foreach (GameObject item in equapmentBar)
        // {
        //     item.SetActive(true);
        // }

    }

    public void ReturnMainMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        SceneManager1.Instance.isBackToMainMenu = true;
        GetComponent<SceneTransition>().LoadScene("GameScene");
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

    public void SetVolume()
    {
        if (AudioManager.Instance != null )
        {
            AudioManager.Instance.SetGlobalVolume(volumeSlider.value);
        }

    }

    void HiddenStartUI()
    {
        StartButton.SetActive(false);
        CreditsButton.SetActive(false);
        ExitButton.SetActive(false);
    }

    void ShowStartUI()
    {
        StartButton.SetActive(true);
        CreditsButton.SetActive(true);
        ExitButton.SetActive(true);
    }

    void ShowGameUI()
    {
        foreach (GameObject item in equapmentBar)
        {
            item.SetActive(true);
        }

        MiniMapMask.SetActive(true);
        MiniMap.SetActive(true);
        SettingsButton.gameObject.SetActive(true);

    }

    void HiddenGameUI()
    {
        foreach (GameObject item in equapmentBar)
        {
            item.SetActive(false);
        }

        MiniMapMask.SetActive(false);
        MiniMap.SetActive(false);
        SettingsButton.gameObject.SetActive(false);
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
