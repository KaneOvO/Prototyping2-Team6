using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantingTool : MonoBehaviour
{
    public Tools tools;
    public GameObject treePrefab;

    void Start()
    {

    }

    void Update()
    {
        if (tools.isPlantingTool)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                PlantingTreeOrDestoryTree();
                
            }
        }
    }

    void PlantingTreeOrDestoryTree()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            if (hit.collider.CompareTag("Planet"))
            {
                GameObject tree = Instantiate(treePrefab, hit.point, Quaternion.identity);

                tree.transform.up = hit.normal;
                //set parent to planet
                tree.transform.SetParent(hit.collider.transform.Find("Trees"));
            }
            else if (hit.collider.CompareTag("Tree"))
            {
                // if(hit.collider.GetComponent<Tree>().isBurned)
                // {
                //     Destroy(hit.collider.gameObject);
                // }
                
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
