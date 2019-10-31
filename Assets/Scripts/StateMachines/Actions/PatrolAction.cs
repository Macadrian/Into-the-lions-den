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
        var unit = controller.enemyController.unit;
        if (!unit.currentPath)
        {
            unit.target = controller.enemyController.wayPointList[controller.enemyController.nextWayPoint];
        }

        if (unit.currentPath)
        {
            unit.currentPath = false;
            controller.enemyController.nextWayPoint = (controller.enemyController.nextWayPoint + 1) % controller.enemyController.wayPointList.Count;
        }
    }
    
}