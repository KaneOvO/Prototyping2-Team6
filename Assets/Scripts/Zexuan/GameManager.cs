using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject player;
    public GameObject planet;
    public bool isThirdPesronView;
    public bool isWorldView;
    public GameObject[] fire;
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
        isThirdPesronView = true;
        isWorldView = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 1)
            {
                UIManager.Instance.PauseGame();
            }
            else
            {
                UIManager.Instance.ResumeGame();
            }
        }
    }
}
