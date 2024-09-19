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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
