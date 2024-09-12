using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dock : MonoBehaviour
{
    public List<Window> windows;
    public GameObject[] dockIcons;

    public void Start()
    {
        DockUpdate();
    }

    public void OpenWindow(Window window)
    {
        windows.Add(window);
        
        DockUpdate();
    }
    
    public void CloseWindow(Window window)
    {
        windows.Remove(window);
        
        DockUpdate();
    }

    private void DockUpdate()
    {
        for (var i = 0; i < dockIcons.Length; i++)
        {
            if (i < windows.Count)
            {
                dockIcons[i].GetComponent<Image>().sprite = windows[i].dockIconSprite;
                dockIcons[i].GetComponent<Button>().onClick.AddListener(windows[i].OnRestoreButtonClicked);
                windows[i].restoreButton = dockIcons[i].GetComponent<Button>();
                
                dockIcons[i].SetActive(true);
            }
            else
            {
                dockIcons[i].SetActive(false);
            }
        }
    }
    
    public void MinimizeWindow(Window window)
    {
        
    }

    public void RestoreWindow(Window window)
    {
        
    }

}
