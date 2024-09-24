using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    public bool isPlantingTool;
    public bool isWateringTool;
    void Start()
    {
        isPlantingTool = true;
        isWateringTool = false;
        UIManager.Instance.plantingToolUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isPlantingTool = true;
            isWateringTool = false;
            UIManager.Instance.plantingToolUI.SetActive(true);
            UIManager.Instance.wateringToolUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isPlantingTool = false;
            isWateringTool = true;
            UIManager.Instance.plantingToolUI.SetActive(false);
            UIManager.Instance.wateringToolUI.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            //Debug.Log("UnLock Cursor");
        }

        if ((Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt)) && Time.timeScale == 1)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //Debug.Log("lock Cursor");
        }
    }
}
