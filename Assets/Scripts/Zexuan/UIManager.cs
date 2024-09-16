using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //singleton
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI toolText;
    public Tools tools;

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
}
