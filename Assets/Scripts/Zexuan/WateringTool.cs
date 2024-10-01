using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Roger;

public class WateringTool : MonoBehaviour
{
    public ToolsManager tools;
    public GameObject wateringUI;
    public float wateringCooldown = 2f;
    public float wateringDuration = 10f;
    public Collider waterCollider1;
    public Collider waterCollider2;
    public GameObject waterEffect;
    public float extinguishTimeThreshold = 2f;
    private float wateringTime = 0f;
    private float cooldownTime = 0f;
    private bool isWatering = false;
    private bool isCoolingDown = false;
    public Animator animator;

    void Update()
    {
        if (tools.isWateringTool && !EventSystem.current.IsPointerOverGameObject() && Time.timeScale == 1 && GameManager.Instance.isThirdPesronView)
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
                    waterCollider1.enabled = true;
                    waterCollider2.enabled = true;
                    waterEffect.SetActive(true);
                    animator.SetBool("IsWatering", true);
                    AudioManager.Instance.Play("Water");
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
        waterCollider1.enabled = false;
        waterCollider2.enabled = false;
        waterEffect.SetActive(false);
        isCoolingDown = true;
        wateringUI.GetComponent<TestSkill>().isCold = true;
        animator.SetBool("IsWatering", false);
        AudioManager.Instance.Stop("Water");
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
