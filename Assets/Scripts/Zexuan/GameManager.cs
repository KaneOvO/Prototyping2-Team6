using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Roger;


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
        if(SceneManager1.Instance.isBackToMainMenu)
        {
            isThirdPesronView = false;
            isWorldView = true;
            player.transform.SetParent(planet.transform);
        }
        else
        {
            isThirdPesronView = true;
            isWorldView = false;
            player.transform.SetParent(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 1 && !UIManager.Instance.isTransitioning && !UIManager.Instance.isMainMenu)
            {
                UIManager.Instance.PauseGame();
            }
            else if(Time.timeScale == 0 && !UIManager.Instance.isTransitioning && !UIManager.Instance.isMainMenu)
            {
                UIManager.Instance.ResumeGame();
            }
        }

        if(Roger.GameManager.Instance.burningTrees.Count > 0)
        {
            AudioManager.Instance.Play("Fire");
        }
        else
        {
            AudioManager.Instance.Stop("Fire");
        }

    }

    
}
