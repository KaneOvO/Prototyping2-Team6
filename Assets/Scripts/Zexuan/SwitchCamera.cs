using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject camera1;
    public GameObject camera2;
    public GameObject startCamera;
    public GameObject minimapCamera1;
    public GameObject minimapCamera2;
    public GameObject camrera2OriginPos;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager1.Instance.isBackToMainMenu)
        {
            camera1.SetActive(false);
            camera2.SetActive(false);
            minimapCamera1.SetActive(false);
            minimapCamera2.SetActive(false);
            startCamera.SetActive(true);
        }
        else
        {
            OnStartGame();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !UIManager.Instance.isTransitioning && !UIManager.Instance.isMainMenu)
        {
            camera1.SetActive(!camera1.activeSelf);
            camera2.SetActive(!camera2.activeSelf);

            camera2.transform.position = camrera2OriginPos.transform.position;
            camera2.transform.rotation = camrera2OriginPos.transform.rotation;

            minimapCamera1.SetActive(!minimapCamera1.activeSelf);
            minimapCamera2.SetActive(!minimapCamera2.activeSelf);

            GameManager.Instance.isThirdPesronView = !GameManager.Instance.isThirdPesronView;
            GameManager.Instance.isWorldView = !GameManager.Instance.isWorldView;

            if (GameManager.Instance.isWorldView)
            {
                GameManager.Instance.player.transform.SetParent(GameManager.Instance.planet.transform);
            }
            else if (GameManager.Instance.isThirdPesronView)
            {
                GameManager.Instance.player.transform.SetParent(null);
            }

            //change culling mask of main camera
            if (GameManager.Instance.isThirdPesronView)
            {
                mainCamera.GetComponent<Camera>().cullingMask = -1;
                mainCamera.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("PlayerTag"));
            }
            else if (GameManager.Instance.isWorldView)
            {
                mainCamera.GetComponent<Camera>().cullingMask = -1;
                mainCamera.GetComponent<Camera>().cullingMask &= ~((1 << LayerMask.NameToLayer("PlayerTag")) | (1 << LayerMask.NameToLayer("Fire")));
            }
        }
    }

    public void OnStartGame()
    {
        camera1.SetActive(true);
        camera2.SetActive(false);
        minimapCamera1.SetActive(false);
        minimapCamera2.SetActive(true);
        startCamera.SetActive(false);
        GameManager.Instance.player.transform.SetParent(null);
    }


}
