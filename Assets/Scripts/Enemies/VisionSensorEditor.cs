using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VisionSensor))]
public class VisionSensorEditor : Editor
{
    private void OnSceneGUI()
    {
        VisionSensor vision = (VisionSensor) target;
        Handles.DrawWireArc(vision.transform.position, Vector3.forward, Vector3.up, 360, vision.radiusVision);
        

        Vector3 viewAngleDirectionA = vision.DirFromAngle(-vision.angleVision / 2, false);
        Vector3 viewAngleDirectionB = vision.DirFromAngle(vision.angleVision / 2, false);

        Handles.DrawLine(vision.transform.position, vision.transform.position + viewAngleDirectionA * vision.radiusVision);
        Handles.DrawLine(vision.transform.position, vision.transform.position + viewAngleDirectionB * vision.radiusVision);

        if (GameManager.Instance != null && vision.IsTargetVisible())
        {
            Handles.color = Color.red;
            Handles.DrawLine(vision.transform.position, GameManager.Instance.playerTransform.position);
        }
    }
}
