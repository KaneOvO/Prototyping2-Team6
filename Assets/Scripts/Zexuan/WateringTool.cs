using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Roger;

public class WateringTool : MonoBehaviour
{
    public ToolsManager tools;
    public GameObject wateringUI;
    public float wateringCooldown = 2f;
    public float wateringDuration = 10f;
    public Collider waterCollider;
    public Renderer meshRenderer;
    public float extinguishTimeThreshold = 2f;
    private float wateringTime = 0f;
    private float cooldownTime = 0f;
    private bool isWatering = false;
    private bool isCoolingDown = false;

    void Update()
    {
        if (tools.isWateringTool)
        {
            if (isCoolingDown)
            {
                cooldownTime += Time.deltaTime;
                if (cooldownTime >= wateringCooldown)
                {
                    isCoolingDown = false;
                    cooldownTime = 0f;
                }
                return;
            }

            if (Input.GetMouseButton(0))
            {
                if (!isWatering)
                {
                    isWatering = true;
                    wateringTime = 0f;
                    waterCollider.enabled = true;
                    meshRenderer.enabled = true;
                }

                wateringTime += Time.deltaTime;

                if (wateringTime >= wateringDuration)
                {
                    StopWatering();
                }

            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopWatering();
            }
        }
    }

    void StopWatering()
    {
        isWatering = false;
        waterCollider.enabled = false;
        meshRenderer.enabled = false;
        isCoolingDown = true;
        wateringUI.GetComponent<TestSkill>().isCold = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tree")
        {
            Roger.Tree tree = other.gameObject.GetComponent<Roger.Tree>();

            if (tree != null)
            {
                tree._treeWatered = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Tree")
        {
            Roger.Tree tree = other.gameObject.GetComponent<Roger.Tree>();

            if (tree != null)
            {
                tree._treeWatered = false;
            }
        }
    }
}
