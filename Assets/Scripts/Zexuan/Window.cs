using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    public Button minimizeButton;
    public Button closeButton;
    public Button windowIconButton;

    public RectTransform windowPanel;
    public Button restoreButton;
    public Vector3 minimizedPosition;
    public float animationDuration = 1f;
    public Vector3 originalPosition = new Vector3(0, 0, 0);
    public bool isMinimized = false;
    public bool isClosed = false;

    void Start()
    {
        windowPanel = gameObject.GetComponent<RectTransform>();

        minimizeButton.onClick.AddListener(MinimizeWindow);
        restoreButton.onClick.AddListener(OnRestoreButtonClicked);
        closeButton.onClick.AddListener(CloseWindow);
    }

    public void MinimizeWindow()
    {
        if (!isMinimized && !isClosed)
        {
            //stop button from being clicked while the window is being minimized
            minimizeButton.interactable = false;
            restoreButton.interactable = false;
            windowIconButton.interactable = false;

            restoreButton.onClick.AddListener(RestoreWindow);
            Vector3 minimizedPosition = restoreButton.transform.position;
            minimizedPosition = windowPanel.parent.InverseTransformPoint(minimizedPosition); //convert the position of the restore button to the local position of the windowPanel

            LeanTween.scale(windowPanel, Vector3.zero, animationDuration);
            LeanTween.move(windowPanel, minimizedPosition, animationDuration).setOnComplete(() =>
            {
                //change the z value of the windowPanel to 1 so that the window is hidden behind the other windows
                windowPanel.localPosition = new Vector3(windowPanel.localPosition.x, windowPanel.localPosition.y, 1);
                isMinimized = true;

                //allow the window to be restored
                minimizeButton.interactable = true;
                restoreButton.interactable = true;
                windowIconButton.interactable = true;
            });
        }

    }

    public void RestoreWindow()
    {
        if (isMinimized && !isClosed)
        {
            //stop button from being clicked while the window is being restored
            minimizeButton.interactable = false;
            restoreButton.interactable = false;
            windowIconButton.interactable = false;

            //change the z value of the windowPanel to 0 so that the window is visible
            windowPanel.localPosition = new Vector3(windowPanel.localPosition.x, windowPanel.localPosition.y, 0);
            //restore the window
            LeanTween.scale(windowPanel, Vector3.one, animationDuration);
            LeanTween.move(windowPanel, originalPosition, animationDuration).setOnComplete(() =>
            {
                isMinimized = false;

                //allow the window to be minimized
                minimizeButton.interactable = true;
                restoreButton.interactable = true;
                windowIconButton.interactable = true;
            });
        }
    }

    public void OnRestoreButtonClicked()
    {
        if (!isMinimized && !isClosed)
        {
            MinimizeWindow();
        }
        else if (isMinimized && !isClosed)
        {
            RestoreWindow();
        }
    }

    public void CloseWindow()
    {
        if (!isClosed)
        {
            //stop button from being clicked while the window is being closed
            closeButton.interactable = false;
            isClosed = true;
            LeanTween.scale(windowPanel, Vector3.zero, animationDuration).setOnComplete(() =>
            {
                //allow the button to be clicked again
                closeButton.interactable = true;

                //change the z value of the windowPanel to 1 so that the window is hidden behind the other windows
                windowPanel.localPosition = new Vector3(windowPanel.localPosition.x, windowPanel.localPosition.y, 1);
            });
        }

    }

    public void OpenWindow()
    {
        if (isClosed)
        { 
            windowIconButton.interactable = false;
            isClosed = false;
            LeanTween.scale(windowPanel, Vector3.one, animationDuration).setOnComplete(() =>
            {
                windowIconButton.interactable = true;

                //change the z value of the windowPanel to 0 so that the window is visible
                windowPanel.localPosition = new Vector3(windowPanel.localPosition.x, windowPanel.localPosition.y, 0);
            });
        }
    }

}
