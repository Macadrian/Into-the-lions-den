using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : AI_Action
{

    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        /*var unit = controller.unit;
        if (!unit.currentPath)
        {
            unit.target = controller.wayPointList[controller.nextWayPoint];
        }

        if (unit.currentPath)
        {
            unit.currentPath = false;
            controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
        }*/
    }
    
}