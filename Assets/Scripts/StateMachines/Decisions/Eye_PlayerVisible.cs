using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Eye_PlayerVisible")]
public class Eye_PlayerVisible : Decision
{
    public override bool Decide(StateController controller)
    {
        return controller.vision.IsTargetVisible(controller.playerTransform) && controller.eyeController.myVision.active && !controller.PlayerInGrass();
    }
}
