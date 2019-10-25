using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/WaitingTime")]
public class WaitingTime : Decision
{
    public override bool Decide(StateController controller)
    {
        return controller.vision.IsTargetVisible(controller.playerTransform) && controller.PlayerInGrass();
    }
}
