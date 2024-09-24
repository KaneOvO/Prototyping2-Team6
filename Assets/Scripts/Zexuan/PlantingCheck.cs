using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingCheck : MonoBehaviour
{
    Renderer spriteRenderer;
    public bool canPlanting = true;

    void Start()
    {
        spriteRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (canPlanting)
        {
            spriteRenderer.material.color = Color.green;
        }
        else
        {
            spriteRenderer.material.color = Color.red;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tree")
        {
            canPlanting = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Tree")
        {
            canPlanting = true;
        }
    }
}
