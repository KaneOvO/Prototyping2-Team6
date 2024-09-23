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
    public Button startButton;
    public Button restartButton;
    public Button CreditsButton;
    public Button exitButton;
    public GameObject fadeImage;


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
}
