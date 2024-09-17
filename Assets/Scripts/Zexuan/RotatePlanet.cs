using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour
{
    public float rotateSpeed = 10f;
    public Transform planet;

    // Start is called before the first frame update
    void Start()
    {
        planet = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isWorldView)
        {
            // Rotate the planet by the input of horizontal axis
            planet.Rotate(GameManager.Instance.player.transform.forward, rotateSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));

            // Rotate the planet by the input of vertical axis
            planet.Rotate(-GameManager.Instance.player.transform.right, rotateSpeed * Time.deltaTime * Input.GetAxis("Vertical"));
        }

    }
}
