using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantingTool : MonoBehaviour
{
    public Tools tools;
    public GameObject treePrefab;
    public GameObject treePreviewPrefab;
    private GameObject currentPreviewTree;
    private bool isPreviewing = false;

    void Start()
    {

    }

    void Update()
    {
        if (tools.isPlantingTool)
        {
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                ShowTreePreview();
            }
            else if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject() && CheckCanPlanting(currentPreviewTree))
            {
                RemoveTreePreview();
                PlantingTreeOrDestoryTree();
                Debug.Log("PlantingTree");
            }
            else if (Input.GetMouseButtonUp(0) && !CheckCanPlanting(currentPreviewTree))
            {
                RemoveTreePreview();
                Debug.Log("RemoveTreePreview");
            }
            else if(!Input.GetMouseButton(0) && isPreviewing)
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
                tree.transform.up = hit.normal;
                tree.transform.SetParent(hit.collider.transform.Find("Trees"));
            }
            else if (hit.collider.CompareTag("Tree"))
            {
                Destroy(hit.collider.gameObject);
                Debug.Log("DestroyTree");
            }
        }
    }

    bool CheckCanPlanting(GameObject tree)
    {
        return tree.transform.Find("PlantingCheck").GetComponent<PlantingCheck>().canPlanting;
    }

}