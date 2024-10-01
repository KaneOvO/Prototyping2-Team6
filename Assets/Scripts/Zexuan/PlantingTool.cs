using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Roger;

public class PlantingTool : MonoBehaviour
{
    public ToolsManager tools;
    public GameObject treePrefab;
    public GameObject treePreviewPrefab;
    public GameObject plantingUI;
    private GameObject currentPreviewTree;
    private bool isPreviewing = false;
    public float plantingCooldown = 2f;
    private float lastPlantingTime = -Mathf.Infinity;

    void Start()
    {

    }

    void Update()
    {
        bool canPlant = Time.time >= lastPlantingTime + plantingCooldown;
        if (tools.isPlantingTool && canPlant && !EventSystem.current.IsPointerOverGameObject() && Cursor.visible && Time.timeScale == 1 && GameManager.Instance.isThirdPesronView)
        {
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                ShowTreePreview();
            }
            else if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject() && CheckCanPlanting(currentPreviewTree))
            {
                RemoveTreePreview();
                PlantingTreeOrDestoryTree();
                lastPlantingTime = Time.time;
                plantingUI.GetComponent<TestSkill>().isCold = true;
                Debug.Log("PlantingTree");
            }
            else if (Input.GetMouseButtonUp(0) && !CheckCanPlanting(currentPreviewTree))
            {
                RemoveTreePreview();
                Debug.Log("RemoveTreePreview");
            }
            else if (!Input.GetMouseButton(0) && isPreviewing)
            {
                RemoveTreePreview();
                Debug.Log("RemoveTreePreview");
            }
        }
        else
        {
            if (isPreviewing)
            {
                RemoveTreePreview();
            }
        }
    }

    void ShowTreePreview()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.tag);
            if (hit.collider.CompareTag("Planet"))
            {
                if (currentPreviewTree == null)
                {

                    currentPreviewTree = Instantiate(treePreviewPrefab, hit.point, Quaternion.identity);
                    isPreviewing = true;
                }
                else
                {

                    currentPreviewTree.transform.position = hit.point;
                    currentPreviewTree.transform.up = hit.normal;
                }
            }

            if (hit.collider.CompareTag("Water"))
            {
                RemoveTreePreview();
            }
        }
    }

    void RemoveTreePreview()
    {
        if (currentPreviewTree != null)
        {
            Destroy(currentPreviewTree);
            currentPreviewTree = null;
            isPreviewing = false;
        }
    }

    void PlantingTreeOrDestoryTree()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.tag);
            if (hit.collider.CompareTag("Planet"))
            {
                GameObject tree = Instantiate(treePrefab, hit.point, Quaternion.identity);
                Roger.GameManager.Instance.TreePlanted(tree.GetComponent<Roger.Tree>());
                tree.transform.up = hit.normal;
                tree.transform.SetParent(GameManager.Instance.planet.transform.Find("Trees"));
                AudioManager.Instance.Play("Planting");
            }
        }
    }

    bool CheckCanPlanting(GameObject tree)
    {
        if (tree == null)
        {
            Debug.LogWarning("Tree is null in CheckCanPlanting");
            return false;
        }

        Transform plantingCheck = tree.transform.Find("PlantingCheck");
        if (plantingCheck == null)
        {
            Debug.LogWarning("PlantingCheck not found in tree");
            return false;
        }

        return plantingCheck.GetComponent<PlantingCheck>().canPlanting;
    }

}