using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/TimeWaitedDecision")]
public class TimeWaitedDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        if (controller.enemyController.timeWaited >= controller.enemyController.waitTime || (controller.vision.IsTargetVisible(controller.playerTransform) && !controller.PlayerInGrass()))
        {
            controller.enemyController.timeWaited = 0;
            controller.enemyController.wait = false;
            return true;
        }
        return false;
    }
}
