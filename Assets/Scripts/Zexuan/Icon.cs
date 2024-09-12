using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour
{
    public Button iconButton;
    public GameObject window;
    public RectTransform windowPanel;
    public Window windowScript;
    private float firstClickTime = 0f;
    private float timeBetweenClicks = 0.3f;
    private bool isOneClick = false;

    void Start()
    {
        iconButton = gameObject.GetComponent<Button>();
        windowPanel = window.GetComponent<RectTransform>();
        windowScript = window.GetComponent<Window>();
        windowScript.windowIconButton = iconButton;

        iconButton.onClick.AddListener(OnDoubleClick);

    }

    public void OnDoubleClick()
    {
        if (isOneClick && (Time.time - firstClickTime) < timeBetweenClicks)
        {
            OpenWindow();
            isOneClick = false;
        }
        else
        {
            isOneClick = true;
            firstClickTime = Time.time;
        }

    }

    public void OpenWindow()
    {
        if (windowScript.isMinimized)
        {
            windowScript.RestoreWindow();
        }
        else if (windowScript.isClosed)
        {
            windowScript.OpenWindow();
        }
    }
}
