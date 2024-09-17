using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera2;
    public GameObject minimapCamera1;
    public GameObject minimapCamera2;
    public GameObject camrera2OriginPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            camera1.SetActive(!camera1.activeSelf);
            camera2.SetActive(!camera2.activeSelf);
            
            camera2.transform.position = camrera2OriginPos.transform.position;
            camera2.transform.rotation = camrera2OriginPos.transform.rotation;

            minimapCamera1.SetActive(!minimapCamera1.activeSelf);
            minimapCamera2.SetActive(!minimapCamera2.activeSelf);

            GameManager.Instance.isThirdPesronView = !GameManager.Instance.isThirdPesronView;
            GameManager.Instance.isWorldView = !GameManager.Instance.isWorldView;

            if(GameManager.Instance.isWorldView)
            {
                GameManager.Instance.player.transform.SetParent(GameManager.Instance.planet.transform);
            }
            else if(GameManager.Instance.isThirdPesronView)
            {
                GameManager.Instance.player.transform.SetParent(null);
            }   
        }
    }


}
