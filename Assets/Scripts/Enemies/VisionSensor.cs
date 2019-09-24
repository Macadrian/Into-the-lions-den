using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionSensor : MonoBehaviour
{
    private GameManager gameManager;

    public float radiusVision = 2f;
    [Range(0,360)] public float angleVision = 90f;

    private float angleOffSet = 90f;

    public LayerMask obstacleMask;
    
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        //IsTargetVisible();
    }

    public bool IsTargetVisible()
    {
        float distanceToTarget = Vector3.Distance(transform.position, gameManager.playerTransform.position);
        if (distanceToTarget < radiusVision)
        {
            Vector3 dirToTarget = (gameManager.playerTransform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.right, dirToTarget);

            if (angle < angleVision / 2)
            {
                if (!Physics2D.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private float distanceFrom(Transform transform, Vector3 globalPosition)
    {
        Vector3 localPosition = transform.InverseTransformPoint(globalPosition);
        float distance = Mathf.Sqrt(localPosition.x * localPosition.x + localPosition.y * localPosition.y);

        return distance;
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z - angleOffSet;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}
