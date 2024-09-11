using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonitorUIManager : MonoBehaviour
{
    //singleton
    public static MonitorUIManager Instance { get; private set; }
    [SerializeField] GameObject[] icons;

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
}
