using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/TimeWaitedDecision")]
public class TimeWaitedDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        if (controller.timeWaited >= controller.waitTime || (controller.vision.IsTargetVisible(controller.playerTransform) && !controller.PlayerInGrass()))
        {
            controller.timeWaited = 0;
            controller.wait = false;
            return true;
        }
        return false;
    }
}
