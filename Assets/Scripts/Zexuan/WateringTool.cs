using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringTool : MonoBehaviour
{
    public Tools tools;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tools.isWateringTool)
        {
            if (Input.GetMouseButtonDown(0))
            {
                WaterTree();
            }
        }
    }

    void WaterTree()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // if (hit.collider.CompareTag("Tree"))
            // {
            //     Tree tree = hit.collider.GetComponent<Tree>();
            //     if (tree != null)
            //     {
            //         if(tree.level != 2 && !tree.isBurning)
            //         {
            //             tree.Water();
            //         }
            //         else if(tree.isBurning)
            //         {
                        
            //         }

            //     }
            // }
        }
    }
}
