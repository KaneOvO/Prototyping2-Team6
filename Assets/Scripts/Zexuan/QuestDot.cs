using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Roger;

public class QuestDot : MonoBehaviour
{
    public Vector3 targetPosition;
    public Transform dotPosition;
    public Camera miniMapCamera;
    public GameObject closestFire;

    void Awake()
    {
        dotPosition = transform;
    }

    private void Update()
    {
        if(UIManager.Instance.isMainMenu)
        {
            dotPosition.localScale = Vector3.zero;
            return;
        }
        
        closestFire = GetClosestTargetOnSphere(Roger.GameManager.Instance.burningTrees, GameManager.Instance.player.transform.position);
        if(closestFire == null)
        {
            dotPosition.localScale = Vector3.zero;
            return;
        }
        targetPosition = closestFire.transform.position;
        dotPosition.localScale = Vector3.one;
        if (closestFire != null)
        {
            if (!IsOutsideMinimap(closestFire.transform.position, GameManager.Instance.player.transform.position, GameManager.Instance.planet.transform.position, 15f))
            {
                dotPosition.position = targetPosition;
                dotPosition.localScale = Vector3.one;
                dotPosition.rotation = closestFire.transform.rotation * Quaternion.Euler(90, 0, 0);

                //Debug.Log("inside");
            }
            else
            {
                // calculate the edge direction
                Vector3 edgeDirection = (closestFire.transform.position - GameManager.Instance.player.transform.position).normalized;

                // Calculate the planet center
                Vector3 planetCenter = GameManager.Instance.planet.transform.position;
                Vector3 playerToSurfaceNormal = (GameManager.Instance.player.transform.position - planetCenter).normalized;

                // Calculate the planet radius
                float planetRadius = Vector3.Distance(GameManager.Instance.player.transform.position, planetCenter);

                // Set the dot position
                dotPosition.position = GameManager.Instance.player.transform.position + edgeDirection * 35f;  

                // Calculate the new position above the surface
                float offsetAboveSurface = 1.0f;  // set the offset above the surface
                dotPosition.position = planetCenter + (dotPosition.position - planetCenter).normalized * (planetRadius + offsetAboveSurface);

                // calculate forward direction
                Vector3 forwardDirection = Vector3.Cross(playerToSurfaceNormal, Vector3.up).normalized;
                if (forwardDirection == Vector3.zero)
                {
                    forwardDirection = Vector3.forward;  // fallback to a default direction
                }

                // Set the rotation of the dot
                dotPosition.rotation = Quaternion.LookRotation(forwardDirection, playerToSurfaceNormal) * Quaternion.Euler(90, 0, 0);

                //Debug.Log("outside");
            }

        }
    }

    public GameObject GetClosestTargetOnSphere(List<Roger.Tree> targets, Vector3 playerPosition)
    {
        GameObject closestTarget = null;
        float minDistance = Mathf.Infinity;

        foreach (var target in targets)
        {
            float distance = Vector3.Distance(playerPosition, target.gameObject.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestTarget = target.gameObject;
            }
        }
        return closestTarget;
    }

    // Vector3 GetDirectionOnSphere(Vector3 targetPosition, Vector3 playerPosition, Vector3 planetCenter)
    // {
    //     Vector3 playerToTarget = (targetPosition - planetCenter).normalized;
    //     Vector3 playerUp = (playerPosition - planetCenter).normalized;
    //     return Vector3.Cross(playerUp, playerToTarget);
    // }

    bool IsOutsideMinimap(Vector3 targetPosition, Vector3 playerPosition, Vector3 planetCenter, float viewAngleLimit)
    {
        Vector3 playerDirection = (playerPosition - planetCenter).normalized;
        Vector3 targetDirection = (targetPosition - planetCenter).normalized;
        
        float angleBetween = Vector3.Angle(playerDirection, targetDirection);

        return angleBetween > viewAngleLimit;
    }
}
